using CM.API.Data;
using CM.API.Models;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class CartServiceTests
{
    private readonly Mock<AppDbContext> _mockContext;
    private readonly Mock<DbSet<Cart>> _mockCartSet;
    private readonly Mock<DbSet<Ticket>> _mockTicketSet;
    private readonly CartService _cartService;

    public CartServiceTests()
    {
        _mockContext = new Mock<AppDbContext>();
        _mockCartSet = new Mock<DbSet<Cart>>();
        _mockTicketSet = new Mock<DbSet<Ticket>>();

        // Setup mocks for DbSet properties
        _mockContext.Setup(c => c.Carts).Returns(_mockCartSet.Object);
        _mockContext.Setup(t => t.Ticket).Returns(_mockTicketSet.Object);

        _cartService = new CartService(_mockContext.Object);
    }

    [Fact]
    public async Task AddTicketToCart_ShouldReturnFalse_WhenCartNotFound()
    {
        // Arrange
        int cartId = 1;
        List<int> ticketIds = new List<int> { 1 };
        int quantity = 2;

        _mockCartSet.Setup(c => c.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Cart, bool>>>(), default))
            .ReturnsAsync((Cart)null); // Cart not found

        // Act
        var result = await _cartService.AddTicketToCart(cartId, ticketIds, quantity);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddTicketToCart_ShouldReturnFalse_WhenTicketNotFound()
    {
        // Arrange
        int cartId = 1;
        List<int> ticketIds = new List<int> { 1 };
        int quantity = 2;

        var cart = new Cart { CartId = cartId };
        _mockCartSet.Setup(c => c.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Cart, bool>>>(), default))
            .ReturnsAsync(cart); // Cart found

        _mockTicketSet.Setup(t => t.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Ticket, bool>>>(), default))
            .ReturnsAsync((Ticket)null); // Ticket not found

        // Act
        var result = await _cartService.AddTicketToCart(cartId, ticketIds, quantity);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddTicketToCart_ShouldReturnFalse_WhenNotEnoughTicketsAvailable()
    {
        // Arrange
        int cartId = 1;
        List<int> ticketIds = new List<int> { 1 };
        int quantity = 2;

        var cart = new Cart { CartId = cartId };
        var ticket = new Ticket { Id = 1, Showtime = new Showtime { TicketsAvailable = 1 } };

        _mockCartSet.Setup(c => c.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Cart, bool>>>(), default))
            .ReturnsAsync(cart); // Cart found

        _mockTicketSet.Setup(t => t.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Ticket, bool>>>(), default))
            .ReturnsAsync(ticket); // Ticket found

        // Act
        var result = await _cartService.AddTicketToCart(cartId, ticketIds, quantity);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddTicketToCart_ShouldUpdateCart_WhenTicketAddedSuccessfully()
    {
        // Arrange
        int cartId = 1;
        List<int> ticketIds = new List<int> { 1 };
        int quantity = 2;

        var cart = new Cart { CartId = cartId, Tickets = new List<Ticket>() };
        var ticket = new Ticket
        {
            Id = 1,
            Showtime = new Showtime { Id = 1, TicketsAvailable = 10 },
            Price = 10,
            Quantity = 0
        };

        _mockCartSet.Setup(c => c.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Cart, bool>>>(), default))
            .ReturnsAsync(cart); // Cart found

        _mockTicketSet.Setup(t => t.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Ticket, bool>>>(), default))
            .ReturnsAsync(ticket); // Ticket found

        // Act
        var result = await _cartService.AddTicketToCart(cartId, ticketIds, quantity);

        // Assert
        Assert.True(result);
        Assert.Single(cart.Tickets);
        Assert.Equal(2, ticket.Quantity);
        Assert.Equal(8, ticket.Showtime.TicketsAvailable); // Check if available tickets reduced
    }

    [Fact]
    public async Task RemoveTicketFromCart_ShouldReturnFalse_WhenCartNotFound()
    {
        // Arrange
        int cartId = 1;
        int ticketId = 1;

        _mockCartSet.Setup(c => c.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Cart, bool>>>(), default))
            .ReturnsAsync((Cart)null); // Cart not found

        // Act
        var result = await _cartService.RemoveTicketFromCart(cartId, ticketId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task RemoveTicketFromCart_ShouldReturnFalse_WhenTicketNotFoundInCart()
    {
        // Arrange
        int cartId = 1;
        int ticketId = 1;

        var cart = new Cart { CartId = cartId, Tickets = new List<Ticket>() };
        _mockCartSet.Setup(c => c.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Cart, bool>>>(), default))
            .ReturnsAsync(cart); // Cart found

        // Act
        var result = await _cartService.RemoveTicketFromCart(cartId, ticketId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task RemoveTicketFromCart_ShouldRemoveTicketFromCart_WhenTicketFound()
    {
        // Arrange
        int cartId = 1;
        int ticketId = 1;

        var showtime = new Showtime { Id = 1, TicketsAvailable = 10 };
        var ticket = new Ticket { Id = ticketId, Showtime = showtime, Quantity = 1 };
        var cart = new Cart { CartId = cartId, Tickets = new List<Ticket> { ticket } };

        _mockCartSet.Setup(c => c.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Cart, bool>>>(), default))
            .ReturnsAsync(cart); // Cart found

        // Act
        var result = await _cartService.RemoveTicketFromCart(cartId, ticketId);

        // Assert
        Assert.True(result);
        Assert.Empty(cart.Tickets); // Ticket removed
        Assert.Equal(11, showtime.TicketsAvailable); // Available tickets updated
    }
}
