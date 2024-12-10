// Import necessary namespaces for the test class
using CM.API.Data;            // Data access layer
using CM.API.Models;          // Models for database entities
using Microsoft.EntityFrameworkCore; // Entity Framework Core for in-memory database
using Xunit;                  // Testing framework
using System.Threading.Tasks; // For async methods
using System.Collections.Generic; // For list collections
using System.Linq;            // For LINQ queries
using System;                 // For GUID and DateTime

// Test class for CartService
public class CartServiceTests
{
    private readonly CartService _cartService;  // Service being tested
    private readonly AppDbContext _context;     // In-memory database context
    private int _userId;                        // Test user ID
    private int _cartId;                        // Test cart ID
    private int _ticketId;                      // Test ticket ID

    // Constructor initializes in-memory database and service
    public CartServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB for test isolation
            .Options;

        _context = new AppDbContext(options); // Initialize database context
        _cartService = new CartService(_context); // Initialize service

        ResetDatabase(); // Clear database before tests
        SeedDatabase();  // Seed initial data
    }

    // Resets the database state before each test
    private void ResetDatabase()
    {
        _context.Database.EnsureDeleted(); // Delete existing DB
        _context.Database.EnsureCreated(); // Recreate DB
    }

    // Seeds initial test data
    private void SeedDatabase()
    {
        // Generate unique IDs for entities
        _userId = Guid.NewGuid().GetHashCode();
        _cartId = Guid.NewGuid().GetHashCode();
        _ticketId = Guid.NewGuid().GetHashCode();

        // Create test user
        var user = new User
        {
            Id = _userId,
            Email = "user@example.com",
            Username = "testuser",
            Password = "ValidPassword123"
        };

        // Create test cart
        var cart = new Cart
        {
            CartId = _cartId,
            UserId = _userId,
            User = user
        };

        // Create test movie and showtime
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

        // Create test ticket
        var ticket = new Ticket
        {
            Id = _ticketId,
            Price = 10.00m,
            ShowtimeId = showtime.Id,
            Showtime = showtime,
            Quantity = 1,
            IsSold = false
        };

        movie.Showtimes = new List<Showtime> { showtime }; // Associate showtime with movie
        cart.Tickets = new List<Ticket> { ticket };       // Add ticket to cart

        // Save entities to context
        _context.Users.Add(user);
        _context.Carts.Add(cart);
        _context.Movies.Add(movie);
        _context.Showtime.Add(showtime);
        _context.Ticket.Add(ticket);
        _context.SaveChanges(); // Save changes to DB
    }

    // Test adding a valid ticket to cart
    [Fact]
    public async Task AddTicketToCart_ShouldAddTicket_WhenValid()
    {
        ResetDatabase(); // Reset DB
        SeedDatabase();  // Seed data

        var result = await _cartService.AddTicketToCart(_cartId, new List<int> { _ticketId }, 1);

        Assert.True(result); // Verify success

        // Fetch updated cart from DB
        var updatedCart = await _context.Carts
            .Include(c => c.Tickets)
            .FirstOrDefaultAsync(c => c.CartId == _cartId);

        Assert.NotNull(updatedCart); // Ensure cart exists

        // Verify ticket quantity is updated
        var updatedTicket = updatedCart.Tickets.FirstOrDefault(t => t.Id == _ticketId);
        Assert.NotNull(updatedTicket);
        Assert.Equal(2, updatedTicket.Quantity); // Ensure quantity incremented
    }

    // Test adding ticket to nonexistent cart fails
    [Fact]
    public async Task AddTicketToCart_ShouldFail_WhenCartNotFound()
    {
        var result = await _cartService.AddTicketToCart(-1, new List<int> { _ticketId }, 2);
        Assert.False(result); // Verify failure
    }

    // Test adding nonexistent ticket fails
    [Fact]
    public async Task AddTicketToCart_ShouldFail_WhenTicketNotFound()
    {
        var result = await _cartService.AddTicketToCart(_cartId, new List<int> { -1 }, 2);
        Assert.False(result); // Verify failure
    }

    // Test removing a valid ticket from cart
    [Fact]
    public async Task RemoveTicketFromCart_ShouldRemoveTicket_WhenValid()
    {
        ResetDatabase(); // Reset DB
        SeedDatabase();  // Seed data

        var result = await _cartService.RemoveTicketFromCart(_cartId, _ticketId);
        Assert.True(result); // Verify success

        // Verify cart is updated
        var updatedCart = await _context.Carts
            .Include(c => c.Tickets)
            .FirstOrDefaultAsync(c => c.CartId == _cartId);

        Assert.NotNull(updatedCart); // Ensure cart exists
        Assert.Empty(updatedCart.Tickets); // Ensure cart is empty
    }

    // Test removing ticket from nonexistent cart fails
    [Fact]
    public async Task RemoveTicketFromCart_ShouldFail_WhenCartNotFound()
    {
        var result = await _cartService.RemoveTicketFromCart(-1, _ticketId);
        Assert.False(result); // Verify failure
    }

    // Test removing nonexistent ticket fails
    [Fact]
    public async Task RemoveTicketFromCart_ShouldFail_WhenTicketNotFound()
    {
        var result = await _cartService.RemoveTicketFromCart(_cartId, -1);
        Assert.False(result); // Verify failure
    }

    // Test retrieving cart by valid ID
    [Fact]
    public async Task GetCartById_ShouldReturnCart_WhenCartExists()
    {
        var cart = await _cartService.GetCartById(_cartId);

        Assert.NotNull(cart); // Verify cart exists
        Assert.Equal(_cartId, cart.CartId); // Verify correct cart ID
    }

    // Test retrieving cart by invalid ID returns null
    [Fact]
    public async Task GetCartById_ShouldReturnNull_WhenCartNotFound()
    {
        var cart = await _cartService.GetCartById(-1);
        Assert.Null(cart); // Verify cart does not exist
    }

    // Test retrieving cart by valid user ID
    [Fact]
    public async Task GetCartByUserId_ShouldReturnCart_WhenUserExists()
    {
        var cart = await _cartService.GetCartByUserId(_userId);

        Assert.NotNull(cart); // Verify cart exists
        Assert.Equal(_userId, cart.UserId); // Verify correct user ID
    }

    // Test retrieving cart by invalid user ID returns null
    [Fact]
    public async Task GetCartByUserId_ShouldReturnNull_WhenUserNotFound()
    {
        var cart = await _cartService.GetCartByUserId(-1);
        Assert.Null(cart); // Verify cart does not exist
    }
}
