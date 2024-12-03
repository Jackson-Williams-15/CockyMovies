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
    private readonly Mock<ICartService> _mockCartService;
    private readonly CartController _controller;

    public CartControllerTests()
    {
        _mockCartService = new Mock<ICartService>();
        _controller = new CartController(_mockCartService.Object);
    }

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

        _mockCartService.Setup(s => s.AddTicketToCart(request.CartId, request.TicketId, request.Quantity))
                         .ReturnsAsync(true);

        // Act
        var result = await _controller.AddTicketToCart(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
    }

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

        _mockCartService.Setup(s => s.AddTicketToCart(request.CartId, request.TicketId, request.Quantity))
                         .ReturnsAsync(false);

        // Act
        var result = await _controller.AddTicketToCart(request);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetCartById_ReturnsOk_WhenCartIsFound()
    {
        // Arrange
        var cartId = 1;
        var cartDto = new CartDto { CartId = cartId, UserId = 1, Tickets = new List<CartTicketDto>() };

        _mockCartService.Setup(s => s.GetCartById(cartId)).ReturnsAsync(cartDto);

        // Act
        var result = await _controller.GetCartById(cartId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(cartDto, okResult.Value);
    }

    [Fact]
    public async Task GetCartById_ReturnsNotFound_WhenCartIsNotFound()
    {
        // Arrange
        var cartId = 1;

        _mockCartService.Setup(s => s.GetCartById(cartId)).ReturnsAsync((CartDto)null);

        // Act
        var result = await _controller.GetCartById(cartId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetCartForCurrentUser_ReturnsOk_WhenCartIsFound()
    {
        // Arrange
        var userEmail = "test@example.com";
        var cart = new Cart { CartId = 1, UserId = 1 };

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
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(cart, okResult.Value);
    }

    [Fact]
    public async Task GetCartForCurrentUser_ReturnsNotFound_WhenCartIsNotFound()
    {
        // Arrange
        var userEmail = "test@example.com";

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
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task RemoveTicketFromCart_ReturnsOk_WhenTicketIsRemovedSuccessfully()
    {
        // Arrange
        var cartId = 1;
        var ticketId = 101;

        _mockCartService.Setup(s => s.RemoveTicketFromCart(cartId, ticketId)).ReturnsAsync(true);

        // Act
        var result = await _controller.RemoveTicketFromCart(cartId, ticketId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.True((bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value, null));
    }

    [Fact]
    public async Task RemoveTicketFromCart_ReturnsNotFound_WhenCartOrTicketNotFound()
    {
        // Arrange
        var cartId = 1;
        var ticketId = 101;

        _mockCartService.Setup(s => s.RemoveTicketFromCart(cartId, ticketId)).ReturnsAsync(false);

        // Act
        var result = await _controller.RemoveTicketFromCart(cartId, ticketId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
