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
    private readonly Mock<ICartService> _mockCartService;
    private readonly Mock<IPaymentService> _mockPaymentService;
    private readonly Mock<IEmailService> _mockEmailService; // Mocked IEmailService
    private readonly Mock<ILogger<CheckoutController>> _mockLogger;
    private readonly CheckoutController _controller;
    private readonly AppDbContext _dbContext;

    public CheckoutControllerTests()
    {
        _mockCartService = new Mock<ICartService>();
        _mockPaymentService = new Mock<IPaymentService>();
        _mockEmailService = new Mock<IEmailService>(); // Initialize mock
        _mockLogger = new Mock<ILogger<CheckoutController>>();

        // Set up in-memory database for testing
        _dbContext = InMemoryDbContextFactory.CreateDbContext();

        _controller = new CheckoutController(
            _mockCartService.Object,
            _mockPaymentService.Object,
            _dbContext,
            _mockLogger.Object,
            _mockEmailService.Object // Pass the mock email service to controller
        );
    }

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

        var cart = new CartDto
        {
            CartId = 1,
            UserId = 1,
            Tickets = tickets
        };

        // Calculate the total price based on tickets
        decimal totalPrice = tickets.Sum(t => t.Price * t.Quantity);

        _mockCartService.Setup(s => s.GetCartById(request.CartId)).ReturnsAsync(cart);
        _mockPaymentService.Setup(s => s.ProcessPayment(It.IsAny<PaymentDetails>())).ReturnsAsync(true);

        // Act
        var result = await _controller.ProcessCheckout(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var orderReceipt = okResult.Value as OrderReceiptDto;

        Assert.NotNull(orderReceipt);
        Assert.Equal(1, orderReceipt.OrderId);
        Assert.Equal(totalPrice, orderReceipt.TotalPrice);
        Assert.Single(orderReceipt.Tickets);
    }

    [Fact]
    public async Task ProcessCheckout_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("CartId", "CartId is required");
        var request = new CheckoutRequestDto();

        // Act
        var result = await _controller.ProcessCheckout(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var error = badRequestResult.Value as SerializableError;

        Assert.NotNull(error);
        Assert.True(error.ContainsKey("CartId"));
        Assert.Contains("CartId is required", error["CartId"] as string[]);
    }

    [Fact]
    public async Task ProcessCheckout_ReturnsNotFound_WhenCartIsNotFound()
    {
        // Arrange
        var request = new CheckoutRequestDto { CartId = 1 };
        _mockCartService.Setup(s => s.GetCartById(request.CartId)).ReturnsAsync((CartDto)null);

        // Act
        var result = await _controller.ProcessCheckout(request);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

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

        _mockCartService.Setup(s => s.GetCartById(request.CartId)).ReturnsAsync(cart);
        _mockPaymentService.Setup(s => s.ProcessPayment(It.IsAny<PaymentDetails>())).ReturnsAsync(false);

        // Act
        var result = await _controller.ProcessCheckout(request);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
