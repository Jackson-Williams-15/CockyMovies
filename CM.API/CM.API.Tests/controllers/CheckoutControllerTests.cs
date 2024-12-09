using CM.API.Controllers;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using CM.API.Data;
using CM.API.Tests.Utilities;

public class CheckoutControllerTests
{
    private readonly Mock<ICartService> _mockCartService;  // Mock for ICartService
    private readonly Mock<IPaymentService> _mockPaymentService;  // Mock for IPaymentService
    private readonly Mock<IEmailService> _mockEmailService;  // Mock for IEmailService
    private readonly Mock<ILogger<CheckoutController>> _mockLogger;  // Mock for ILogger
    private readonly CheckoutController _controller;  // The controller under test
    private readonly AppDbContext _dbContext;  // In-memory database context for testing

    // Constructor to set up mock services, logger, and the controller instance
    public CheckoutControllerTests()
    {
        _mockCartService = new Mock<ICartService>();
        _mockPaymentService = new Mock<IPaymentService>();
        _mockEmailService = new Mock<IEmailService>();
        _mockLogger = new Mock<ILogger<CheckoutController>>();

        // Set up in-memory database for testing
        _dbContext = InMemoryDbContextFactory.CreateDbContext();

        _controller = new CheckoutController(
            _mockCartService.Object,
            _mockPaymentService.Object,
            _dbContext,
            _mockLogger.Object,
            _mockEmailService.Object  // Pass the mock email service to controller
        );
    }

    // Test for successful checkout processing
    [Fact]
    public async Task ProcessCheckout_ReturnsOk_WhenCheckoutIsSuccessful()
    {
        // Arrange
        var request = new CheckoutRequestDto
        {
            CartId = 1,
            UserId = 1,
            PaymentDetails = new PaymentDetailsDto
            {
                CardNumber = "1234567890123456",
                ExpiryDate = "12/25",
                CVV = "123",
                CardHolderName = "John Doe"
            }
        };

        // Create a list of tickets in the cart
        var tickets = new List<CartTicketDto>
        {
            new CartTicketDto 
            { 
                Id = 101, 
                Price = 10, 
                Quantity = 2, 
                ShowtimeId = 1, 
                MovieId = 1 
            }
        };

        // Create a cart DTO with the tickets
        var cart = new CartDto
        {
            CartId = 1,
            UserId = 1,
            Tickets = tickets
        };

        // Calculate total price based on the tickets
        decimal totalPrice = tickets.Sum(t => t.Price * t.Quantity);

        // Mock the GetCartById and ProcessPayment methods to simulate successful checkout
        _mockCartService.Setup(s => s.GetCartById(request.CartId)).ReturnsAsync(cart);
        _mockPaymentService.Setup(s => s.ProcessPayment(It.IsAny<PaymentDetails>())).ReturnsAsync(true);

        // Act
        var result = await _controller.ProcessCheckout(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);  // Expect Ok response
        var orderReceipt = okResult.Value as OrderReceiptDto;  // Extract the order receipt

        Assert.NotNull(orderReceipt);  // Ensure the receipt is not null
        Assert.Equal(1, orderReceipt.OrderId);  // Ensure the order ID is correct
        Assert.Equal(totalPrice, orderReceipt.TotalPrice);  // Validate the total price
        Assert.Single(orderReceipt.Tickets);  // Ensure there's only one ticket in the order receipt
    }

    // Test for invalid model state
    [Fact]
    public async Task ProcessCheckout_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("CartId", "CartId is required");
        var request = new CheckoutRequestDto();  // Invalid request with no CartId

        // Act
        var result = await _controller.ProcessCheckout(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);  // Expect BadRequest response
        var error = badRequestResult.Value as SerializableError;  // Capture the error details

        Assert.NotNull(error);  // Ensure the error is not null
        Assert.True(error.ContainsKey("CartId"));  // Ensure the error contains "CartId"
        Assert.Contains("CartId is required", error["CartId"] as string[]);  // Check the error message
    }

    // Test for checkout when cart is not found
    [Fact]
    public async Task ProcessCheckout_ReturnsNotFound_WhenCartIsNotFound()
    {
        // Arrange
        var request = new CheckoutRequestDto { CartId = 1 };  // Cart ID 1 does not exist
        _mockCartService.Setup(s => s.GetCartById(request.CartId)).ReturnsAsync((CartDto)null);

        // Act
        var result = await _controller.ProcessCheckout(request);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);  // Expect NotFound response
    }

    // Test for checkout when payment processing fails
    [Fact]
    public async Task ProcessCheckout_ReturnsBadRequest_WhenPaymentFails()
    {
        // Arrange
        var request = new CheckoutRequestDto
        {
            CartId = 1,
            UserId = 1,
            PaymentDetails = new PaymentDetailsDto
            {
                CardNumber = "1234567890123456",
                ExpiryDate = "12/25",
                CVV = "123",
                CardHolderName = "John Doe"
            }
        };

        // Create a list of tickets in the cart
        var tickets = new List<CartTicketDto>
        {
            new CartTicketDto { Id = 101, Price = 10, Quantity = 2, ShowtimeId = 1, MovieId = 1 }
        };
        var cart = new CartDto
        {
            CartId = 1,
            UserId = 1,
            Tickets = tickets
        };

        // Mock the GetCartById method and simulate payment failure
        _mockCartService.Setup(s => s.GetCartById(request.CartId)).ReturnsAsync(cart);
        _mockPaymentService.Setup(s => s.ProcessPayment(It.IsAny<PaymentDetails>())).ReturnsAsync(false);

        // Act
        var result = await _controller.ProcessCheckout(request);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);  // Expect BadRequest response
    }
}
