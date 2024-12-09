using CM.API.Controllers;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

public class CartControllerTests
{
    private readonly Mock<ICartService> _mockCartService; // Mock for ICartService
    private readonly CartController _controller; // Instance of CartController to test

    // Constructor to initialize mocks and the controller instance
    public CartControllerTests()
    {
        _mockCartService = new Mock<ICartService>();
        _controller = new CartController(_mockCartService.Object); // Initialize controller with mocked CartService
    }

    // Test for AddTicketToCart method when ticket is added successfully
    [Fact]
    public async Task AddTicketToCart_ReturnsOk_WhenTicketIsAddedSuccessfully()
    {
        // Arrange
        var request = new AddTicketToCartRequest
        {
            CartId = 1,
            TicketId = new List<int> { 101 },
            Quantity = 2
        };

        // Mock AddTicketToCart to return true (ticket added successfully)
        _mockCartService.Setup(s => s.AddTicketToCart(request.CartId, request.TicketId, request.Quantity))
                         .ReturnsAsync(true);

        // Act
        var result = await _controller.AddTicketToCart(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);  // Check if the result is Ok
        Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));  // Ensure success flag is true
    }

    // Test for AddTicketToCart when ticket is not found or capacity is reached
    [Fact]
    public async Task AddTicketToCart_ReturnsNotFound_WhenTicketNotFoundOrCapacityReached()
    {
        // Arrange
        var request = new AddTicketToCartRequest
        {
            CartId = 1,
            TicketId = new List<int> { 101 },
            Quantity = 2
        };

        // Mock AddTicketToCart to return false (ticket not found or capacity reached)
        _mockCartService.Setup(s => s.AddTicketToCart(request.CartId, request.TicketId, request.Quantity))
                         .ReturnsAsync(false);

        // Act
        var result = await _controller.AddTicketToCart(request);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);  // Expect NotFound response
    }

    // Test for GetCartById method when the cart is found
    [Fact]
    public async Task GetCartById_ReturnsOk_WhenCartIsFound()
    {
        // Arrange
        var cartId = 1;
        var cartDto = new CartDto { CartId = cartId, UserId = 1, Tickets = new List<CartTicketDto>() };

        // Mock GetCartById to return the cartDto
        _mockCartService.Setup(s => s.GetCartById(cartId)).ReturnsAsync(cartDto);

        // Act
        var result = await _controller.GetCartById(cartId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);  // Expect Ok response
        Assert.Equal(cartDto, okResult.Value);  // Ensure the returned value matches the mock
    }

    // Test for GetCartById method when the cart is not found
    [Fact]
    public async Task GetCartById_ReturnsNotFound_WhenCartIsNotFound()
    {
        // Arrange
        var cartId = 1;

        // Mock GetCartById to return null (cart not found)
        _mockCartService.Setup(s => s.GetCartById(cartId)).ReturnsAsync((CartDto)null);

        // Act
        var result = await _controller.GetCartById(cartId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);  // Expect NotFound response
    }

    // Test for GetCartForCurrentUser when the cart is found
    [Fact]
    public async Task GetCartForCurrentUser_ReturnsOk_WhenCartIsFound()
    {
        // Arrange
        var userEmail = "test@example.com";
        var cart = new Cart { CartId = 1, UserId = 1 };

        // Mock GetCartForCurrentUser to return the cart
        _mockCartService.Setup(s => s.GetCartForCurrentUser(userEmail)).ReturnsAsync(cart);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] 
        {
            new Claim(ClaimTypes.Name, userEmail)
        }));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        // Act
        var result = await _controller.GetCartForCurrentUser();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);  // Expect Ok response
        Assert.Equal(cart, okResult.Value);  // Ensure the returned cart matches the mock
    }

    // Test for GetCartForCurrentUser when the cart is not found
    [Fact]
    public async Task GetCartForCurrentUser_ReturnsNotFound_WhenCartIsNotFound()
    {
        // Arrange
        var userEmail = "test@example.com";

        // Mock GetCartForCurrentUser to return null (cart not found)
        _mockCartService.Setup(s => s.GetCartForCurrentUser(userEmail)).ReturnsAsync((Cart)null);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] 
        {
            new Claim(ClaimTypes.Name, userEmail)
        }));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        // Act
        var result = await _controller.GetCartForCurrentUser();

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);  // Expect NotFound response
    }

    // Test for RemoveTicketFromCart when ticket is removed successfully
    [Fact]
    public async Task RemoveTicketFromCart_ReturnsOk_WhenTicketIsRemovedSuccessfully()
    {
        // Arrange
        var cartId = 1;
        var ticketId = 101;

        // Mock RemoveTicketFromCart to return true (ticket removed successfully)
        _mockCartService.Setup(s => s.RemoveTicketFromCart(cartId, ticketId)).ReturnsAsync(true);

        // Act
        var result = await _controller.RemoveTicketFromCart(cartId, ticketId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);  // Expect Ok response
        Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));  // Ensure success flag is true
    }

    // Test for RemoveTicketFromCart when cart or ticket is not found
    [Fact]
    public async Task RemoveTicketFromCart_ReturnsNotFound_WhenCartOrTicketNotFound()
    {
        // Arrange
        var cartId = 1;
        var ticketId = 101;

        // Mock RemoveTicketFromCart to return false (cart or ticket not found)
        _mockCartService.Setup(s => s.RemoveTicketFromCart(cartId, ticketId)).ReturnsAsync(false);

        // Act
        var result = await _controller.RemoveTicketFromCart(cartId, ticketId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);  // Expect NotFound response
    }
}
