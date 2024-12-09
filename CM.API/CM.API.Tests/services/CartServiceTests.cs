using CM.API.Data;
using CM.API.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

public class CartServiceTests
{
    private readonly CartService _cartService;
    private readonly AppDbContext _context;

    public CartServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new AppDbContext(options);
        _cartService = new CartService(_context);

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        var user = new User { Id = 1, Email = "user@example.com" };
        var cart = new Cart { CartId = 1, UserId = 1, User = user };
        var movie = new Movie
        {
            Id = 1,
            Title = "Test Movie",
            DateReleased = DateTime.Now,
            RatingId = 1,
            Showtimes = new List<Showtime>()
        };
        var showtime = new Showtime
        {
            Id = 1,
            StartTime = DateTime.Now.AddDays(1),
            Capacity = 100,
            TicketsAvailable = 100,
            MovieId = 1,
            Movie = movie,
            Tickets = new List<Ticket>()
        };
        var ticket = new Ticket
        {
            Id = 1,
            Price = 10.00m,
            ShowtimeId = 1,
            Showtime = showtime,
            Quantity = 1,
            IsSold = false
        };

        movie.Showtimes.Add(showtime);
        cart.Tickets.Add(ticket);

        _context.Users.Add(user);
        _context.Carts.Add(cart);
        _context.Movies.Add(movie);
        _context.Showtime.Add(showtime);
        _context.Ticket.Add(ticket);
        _context.SaveChanges();
    }

    [Fact]
    public async Task AddTicketToCart_ShouldAddTicket_WhenValid()
    {
        // Act
        var result = await _cartService.AddTicketToCart(1, new List<int> { 1 }, 2);

        // Assert
        Assert.True(result);
        var updatedCart = await _context.Carts.Include(c => c.Tickets).FirstAsync();
        Assert.NotNull(updatedCart.Tickets.FirstOrDefault(t => t.Id == 1));
        Assert.Equal(2, updatedCart.Tickets.First(t => t.Id == 1).Quantity);
    }

    [Fact]
    public async Task AddTicketToCart_ShouldFail_WhenCartNotFound()
    {
        // Act
        var result = await _cartService.AddTicketToCart(999, new List<int> { 1 }, 2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddTicketToCart_ShouldFail_WhenTicketNotFound()
    {
        // Act
        var result = await _cartService.AddTicketToCart(1, new List<int> { 999 }, 2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddTicketToCart_ShouldFail_WhenTicketsUnavailable()
    {
        // Act
        var result = await _cartService.AddTicketToCart(1, new List<int> { 1 }, 101);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task RemoveTicketFromCart_ShouldRemoveTicket_WhenValid()
    {
        // Act
        var result = await _cartService.RemoveTicketFromCart(1, 1);

        // Assert
        Assert.True(result);
        var updatedCart = await _context.Carts.Include(c => c.Tickets).FirstAsync();
        Assert.Empty(updatedCart.Tickets);
    }

    [Fact]
    public async Task RemoveTicketFromCart_ShouldFail_WhenCartNotFound()
    {
        // Act
        var result = await _cartService.RemoveTicketFromCart(999, 1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task RemoveTicketFromCart_ShouldFail_WhenTicketNotFound()
    {
        // Act
        var result = await _cartService.RemoveTicketFromCart(1, 999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetCartById_ShouldReturnCart_WhenCartExists()
    {
        // Act
        var cart = await _cartService.GetCartById(1);

        // Assert
        Assert.NotNull(cart);
        Assert.Equal(1, cart.CartId);
    }

    [Fact]
    public async Task GetCartById_ShouldReturnNull_WhenCartNotFound()
    {
        // Act
        var cart = await _cartService.GetCartById(999);

        // Assert
        Assert.Null(cart);
    }

    [Fact]
    public async Task GetCartByUserId_ShouldReturnCart_WhenUserExists()
    {
        // Act
        var cart = await _cartService.GetCartByUserId(1);

        // Assert
        Assert.NotNull(cart);
        Assert.Equal(1, cart.UserId);
    }

    [Fact]
    public async Task GetCartByUserId_ShouldReturnNull_WhenUserNotFound()
    {
        // Act
        var cart = await _cartService.GetCartByUserId(999);

        // Assert
        Assert.Null(cart);
    }

    [Fact]
    public async Task GetCartForCurrentUser_ShouldCreateCart_WhenNotFound()
    {
        // Act
        var cart = await _cartService.GetCartForCurrentUser("newuser@example.com");

        // Assert
        Assert.NotNull(cart);
        Assert.Equal("newuser@example.com", cart.User.Email);
    }
}
