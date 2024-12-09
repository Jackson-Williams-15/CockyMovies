// Import necessary namespaces for the test class
using CM.API.Data; // Data layer for database context
using CM.API.Models; // Models representing database entities
using CM.API.Services; // Services handling business logic
using Microsoft.EntityFrameworkCore; // Entity Framework Core for in-memory database
using System; // For GUIDs and DateTime
using System.Collections.Generic; // For collections
using System.Linq; // For LINQ queries
using System.Threading.Tasks; // For async methods
using Xunit; // Testing framework

// Test class for TicketService
public class TicketServiceTests
{
    private readonly TicketService _ticketService; // Service being tested
    private readonly AppDbContext _context; // In-memory database context

    // Constructor initializes in-memory database and service
    public TicketServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB instance per test
            .Options;

        _context = new AppDbContext(options); // Create database context
        _ticketService = new TicketService(_context); // Create service with context
    }

    // Resets the database before each test
    private void ResetDatabase()
    {
        _context.Database.EnsureDeleted(); // Delete database
        _context.Database.EnsureCreated(); // Recreate database
    }

    // Seeds database with sample data and returns entity IDs
    private (int MovieId, int ShowtimeId, int TicketId) SeedDatabase()
    {
        // Generate unique IDs
        var movieId = Guid.NewGuid().GetHashCode();
        var showtimeId = Guid.NewGuid().GetHashCode();
        var ticketId = Guid.NewGuid().GetHashCode();

        // Create sample entities
        var movie = new Movie
        {
            Id = movieId,
            Title = "Test Movie",
            DateReleased = DateTime.Now,
            RatingId = Guid.NewGuid().GetHashCode()
        };

        var showtime = new Showtime
        {
            Id = showtimeId,
            StartTime = DateTime.Now.AddDays(1),
            Capacity = 100,
            TicketsAvailable = 90,
            MovieId = movieId,
            Movie = movie
        };

        var ticket = new Ticket
        {
            Id = ticketId,
            Price = 10.00m,
            ShowtimeId = showtimeId,
            Showtime = showtime
        };

        // Add entities to context and save changes
        _context.Movies.Add(movie);
        _context.Showtime.Add(showtime);
        _context.Ticket.Add(ticket);
        _context.SaveChanges();

        return (movieId, showtimeId, ticketId); // Return generated IDs
    }

    // Test to ensure adding a valid ticket works
    [Fact]
    public async Task AddTicket_ShouldAddTicket_WhenValid()
    {
        ResetDatabase(); // Reset DB
        var (_, showtimeId, _) = SeedDatabase(); // Seed data

        // Create a new ticket
        var newTicketId = Guid.NewGuid().GetHashCode();
        var ticket = new Ticket
        {
            Id = newTicketId,
            Price = 15.00m,
            ShowtimeId = showtimeId
        };

        // Call service and assert success
        var result = await _ticketService.AddTicket(ticket);
        Assert.True(result);
        Assert.NotNull(await _context.Ticket.FindAsync(newTicketId));
    }

    // Test to ensure duplicate ticket addition fails
    [Fact]
    public async Task AddTicket_ShouldFail_WhenTicketAlreadyExists()
    {
        ResetDatabase(); // Reset DB
        var (_, showtimeId, ticketId) = SeedDatabase(); // Seed data

        // Create a duplicate ticket
        var ticket = new Ticket
        {
            Id = ticketId,
            Price = 10.00m,
            ShowtimeId = showtimeId
        };

        // Call service and assert failure
        var result = await _ticketService.AddTicket(ticket);
        Assert.False(result);
    }

    // Test to ensure editing a nonexistent ticket fails
    [Fact]
    public async Task EditTicket_ShouldFail_WhenTicketNotFound()
    {
        ResetDatabase(); // Reset DB

        // Attempt to edit a ticket with invalid ID
        var result = await _ticketService.EditTicket(-1, 20.00m);
        Assert.False(result);
    }

    // Test to ensure fetching an existing ticket works
    [Fact]
    public async Task GetTicketById_ShouldReturnTicket_WhenExists()
    {
        ResetDatabase(); // Reset DB
        var (_, _, ticketId) = SeedDatabase(); // Seed data

        // Fetch ticket by ID and assert success
        var ticket = await _ticketService.GetTicketById(ticketId);
        Assert.NotNull(ticket);
        Assert.Equal(ticketId, ticket.Id);
    }

    // Test to ensure fetching a nonexistent ticket returns null
    [Fact]
    public async Task GetTicketById_ShouldReturnNull_WhenNotFound()
    {
        ResetDatabase(); // Reset DB

        // Fetch ticket by invalid ID and assert null result
        var ticket = await _ticketService.GetTicketById(-1);
        Assert.Null(ticket);
    }

    // Test to ensure removing tickets from a nonexistent showtime fails
    [Fact]
    public async Task RemoveTicketsFromShowtime_ShouldFail_WhenShowtimeNotFound()
    {
        ResetDatabase(); // Reset DB

        // Attempt to remove tickets from invalid showtime ID
        var result = await _ticketService.Rem