using CM.API.Data;
using CM.API.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

public class CartServiceTests
{
    private readonly CartService _cartService;
    private readonly AppDbContext _context;
    private int _userId;
    private int _cartId;
    private int _ticketId;

    public CartServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _cartService = new CartService(_context);

        ResetDatabase();
        SeedDatabase();
    }

    private void ResetDatabase()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    private void SeedDatabase()
    {
        _userId = Guid.NewGuid().GetHashCode();
        _cartId = Guid.NewGuid().GetHashCode();
        _ticketId = Guid.NewGuid().GetHashCode();

        var user = new User
        {
            Id = _userId,
            Email = "user@example.com",
            Username = "testuser",
            Password = "ValidPassword123"
        };

        var cart = new Cart
        {
            CartId = _cartId,
            UserId = _userId,
            User = user
        };

        var movie = new Movie
        {
            Id = Guid.NewGuid().GetHashCode(),
            Title = "Test Movie",
            DateReleased = DateTime.Now
        };

        var showtime = new Showtime
        {
            Id = Guid.NewGuid().GetHashCode(),
            StartTime = DateTime.Now.AddDays(1),
            Capacity = 100,
            TicketsAvailable = 100,
            MovieId = movie.Id,
            Movie = movie
        };

        var ticket = new Ticket
        {
            Id = _ticketId,
            Price = 10.00m,
            ShowtimeId = showtime.Id,
            Showtime = showtime,
            Quantity = 1,
            IsSold = false
        };

        movie.Showtimes = new List<Showtime> { showtime };
        cart.Tickets = new List<Ticket> { ticket };

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
        ResetDatabase();
        SeedDatabase();

        var result = await _cartService.AddTicketToCart(_cartId, new List<int> { _ticketId }, 1);

        Assert.True(result);

        var updatedCart = await _context.Carts
            .Include(c => c.Tickets)
            .FirstOrDefaultAsync(c => c.CartId == _cartId);

        Assert.NotNull(updatedCart);

        var updatedTicket = updatedCart.Tickets.FirstOrDefault(t => t.Id == _ticketId);
        Assert.NotNull(updatedTicket);

        // Corrected expected quantity based on initial seed
        Assert.Equal(2, updatedTicket.Quantity);
    }

    [Fact]
    public async Task AddTicketToCart_ShouldFail_WhenCartNotFound()
    {
        var result = await _cartService.AddTicketToCart(-1, new List<int> { _ticketId }, 2);
        Assert.False(result);
    }

    [Fact]
    public async Task AddTicketToCart_ShouldFail_WhenTicketNotFound()
    {
        var result = await _cartService.AddTicketToCart(_cartId, new List<int> { -1 }, 2);
        Assert.False(result);
    }

    [Fact]
    public async Task RemoveTicketFromCart_ShouldRemoveTicket_WhenValid()
    {
        ResetDatabase();
        SeedDatabase();

        var result = await _cartService.RemoveTicketFromCart(_cartId, _ticketId);
        Assert.True(result);

        var updatedCart = await _context.Carts
            .Include(c => c.Tickets)
            .FirstOrDefaultAsync(c => c.CartId == _cartId);

        Assert.NotNull(updatedCart);
        Assert.Empty(updatedCart.Tickets);
    }

    [Fact]
    public async Task RemoveTicketFromCart_ShouldFail_WhenCartNotFound()
    {
        var result = await _cartService.RemoveTicketFromCart(-1, _ticketId);
        Assert.False(result);
    }

    [Fact]
    public async Task RemoveTicketFromCart_ShouldFail_WhenTicketNotFound()
    {
        var result = await _cartService.RemoveTicketFromCart(_cartId, -1);
        Assert.False(result);
    }

    [Fact]
    public async Task GetCartById_ShouldReturnCart_WhenCartExists()
    {
        var cart = await _cartService.GetCartById(_cartId);

        Assert.NotNull(cart);
        Assert.Equal(_cartId, cart.CartId);
    }

    [Fact]
    public async Task GetCartById_ShouldReturnNull_WhenCartNotFound()
    {
        var cart = await _cartService.GetCartById(-1);
        Assert.Null(cart);
    }

    [Fact]
    public async Task GetCartByUserId_ShouldReturnCart_WhenUserExists()
    {
        var cart = await _cartService.GetCartByUserId(_userId);

        Assert.NotNull(cart);
        Assert.Equal(_userId, cart.UserId);
    }

    [Fact]
    public async Task GetCartByUserId_ShouldReturnNull_WhenUserNotFound()
    {
        var cart = await _cartService.GetCartByUserId(-1);
        Assert.Null(cart);
    }
}
