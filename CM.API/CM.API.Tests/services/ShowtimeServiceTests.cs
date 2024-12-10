// Import necessary namespaces for the test class
using CM.API.Models; // Models for database entities
using CM.API.Services; // Business logic services
using CM.API.Data; // Database context
using CM.API.Interfaces; // Interfaces for dependency injection
using Microsoft.EntityFrameworkCore; // Entity Framework Core for in-memory database
using Moq; // Mocking framework
using Xunit; // Testing framework
using System; // For GUIDs and DateTime
using System.Collections.Generic; // For collections
using System.Linq; // For LINQ queries
using System.Threading.Tasks; // For async methods

// Test class for ShowtimeService
public class ShowtimeServiceTests
{
    private readonly ShowtimeService _showtimeService; // Service under test
    private readonly AppDbContext _context; // In-memory database context
    private readonly Mock<IMovieService> _mockMovieService; // Mocked movie service dependency

    // Constructor initializes in-memory database and mocked service
    public ShowtimeServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique DB instance
            .Options;

        _context = new AppDbContext(options); // Initialize database context
        _mockMovieService = new Mock<IMovieService>(); // Mock movie service
        _showtimeService = new ShowtimeService(_context, _mockMovieService.Object); // Create service
    }

    // Reset database state before each test
    private void ResetDatabase()
    {
        _context.Database.EnsureDeleted(); // Delete database
        _context.Database.EnsureCreated(); // Recreate database
    }

    // Seed database with a test movie
    private Movie SeedMovie()
    {
        var movie = new Movie
        {
            Id = Guid.NewGuid().GetHashCode(),
            Title = "Test Movie",
            DateReleased = DateTime.Now,
            RatingId = Guid.NewGuid().GetHashCode()
        };

        _context.Movies.Add(movie); // Add movie to context
        _context.SaveChanges(); // Save changes
        return movie; // Return seeded movie
    }

    // Test adding a valid showtime
    [Fact]
    public async Task AddShowtime_ShouldAddShowtime_WhenValid()
    {
        ResetDatabase(); // Reset DB
        var movie = SeedMovie(); // Seed movie data

        var showtime = new Showtime
        {
            Id = Guid.NewGuid().GetHashCode(),
            StartTime = DateTime.Now.AddHours(3),
            Capacity = 100,
            MovieId = movie.Id
        };

        _mockMovieService.Setup(m => m.GetMovieById(movie.Id)).ReturnsAsync(movie); // Mock movie lookup

        var result = await _showtimeService.AddShowtime(showtime); // Call service

        Assert.True(result); // Verify success
        Assert.Contains(showtime, _context.Showtime); // Verify showtime exists in DB
    }

    // Test adding a duplicate showtime fails
    [Fact]
    public async Task AddShowtime_ShouldFail_WhenShowtimeAlreadyExists()
    {
        ResetDatabase(); // Reset DB
        var movie = SeedMovie(); // Seed movie data
        var showtimeId = Guid.NewGuid().GetHashCode();

        var showtime = new Showtime
        {
            Id = showtimeId,
            StartTime = DateTime.Now.AddHours(3),
            Capacity = 100,
            MovieId = movie.Id
        };

        _context.Showtime.Add(showtime); // Add showtime to context
        _context.SaveChanges(); // Save changes

        var result = await _showtimeService.AddShowtime(showtime); // Call service

        Assert.False(result); // Verify failure
    }

    // Test removing a valid showtime
    [Fact]
    public async Task RemoveShowtime_ShouldRemoveShowtime_WhenValid()
    {
        ResetDatabase(); // Reset DB
        var movie = SeedMovie(); // Seed movie data
        var showtimeId = Guid.NewGuid().GetHashCode();

        var showtime = new Showtime
        {
            Id = showtimeId,
            StartTime = DateTime.Now.AddHours(4),
            Capacity = 100,
            MovieId = movie.Id
        };

        _context.Showtime.Add(showtime); // Add showtime to context
        _context.SaveChanges(); // Save changes

        var result = await _showtimeService.RemoveShowtime(showtimeId); // Call service

        Assert.True(result); // Verify success
        Assert.DoesNotContain(showtime, _context.Showtime); // Verify removal from DB
    }

    // Test removing a nonexistent showtime fails
    [Fact]
    public async Task RemoveShowtime_ShouldFail_WhenShowtimeNotFound()
    {
        ResetDatabase(); // Reset DB

        var result = await _showtimeService.RemoveShowtime(Guid.NewGuid().GetHashCode()); // Call service

        Assert.False(result); // Verify failure
    }

    // Test editing a valid showtime
    [Fact]
    public async Task EditShowtime_ShouldUpdateShowtime_WhenValid()
    {
        ResetDatabase(); // Reset DB
        var movie = SeedMovie(); // Seed movie data
        var showtimeId = Guid.NewGuid().GetHashCode();

        var showtime = new Showtime
        {
            Id = showtimeId,
            StartTime = DateTime.Now.AddHours(2),
            Capacity = 100,
            MovieId = movie.Id
        };

        _context.Showtime.Add(showtime); // Add showtime to context
        _context.SaveChanges(); // Save changes

        var updatedShowtime = new Showtime
        {
            StartTime = DateTime.Now.AddHours(5),
            Capacity = 150,
            MovieId = movie.Id
        };

        var result = await _showtimeService.EditShowtime(showtimeId, updatedShowtime); // Call service

        Assert.True(result); // Verify success

        // Verify updated values
        var editedShowtime = await _context.Showtime.FirstAsync(s => s.Id == showtimeId);
        Assert.Equal(150, editedShowtime.Capacity);
        Assert.Equal(updatedShowtime.StartTime, editedShowtime.StartTime);
    }

    // Test editing a nonexistent showtime fails
    [Fact]
    public async Task EditShowtime_ShouldFail_WhenShowtimeNotFound()
    {
        ResetDatabase(); // Reset DB

        var updatedShowtime = new Showtime
        {
            StartTime = DateTime.Now.AddHours(6),
            Capacity = 120
        };

        var result = await _showtimeService.EditShowtime(Guid.NewGuid().GetHashCode(), updatedShowtime); // Call service

        Assert.False(result); // Verify failure
    }

    // Test fetching showtimes by movie ID when the movie exists
    [Fact]
    public async Task GetShowtimesByMovieId_ShouldReturnShowtimes_WhenMovieExists()
    {
        ResetDatabase(); // Reset DB
        var movie = SeedMovie(); // Seed movie data
        var showtimeId = Guid.NewGuid().GetHashCode();

        var showtime = new Showtime
        {
            Id = showtimeId,
            StartTime = DateTime.Now.AddHours(1),
            Capacity = 100,
            MovieId = movie.Id
        };

        _context.Showtime.Add(showtime); // Add showtime to context
        _context.SaveChanges(); // Save changes

        var showtimes = await _showtimeService.GetShowtimesByMovieId(movie.Id); // Call service

        Assert.NotNull(showtimes); // Verify showtimes exist
        Assert.Single(showtimes); // Verify only one showtime exists
        Assert.Equal(showtimeId, showtimes.First().Id); // Verify correct showtime ID
    }

    // Test fetching showtimes by nonexistent movie ID returns empty
    [Fact]
    public async Task GetShowtimesByMovieId_ShouldReturnEmpty_WhenMovieNotFound()
    {
        ResetDatabase(); // Reset DB

        var showtimes = await _showtimeService.GetShowtimesByMovieId(Guid.NewGuid().GetHashCode()); // Call service

        Assert.NotNull(showtimes); // Verify list is not null
        Assert.Empty(showtimes); // Verify list is empty
    }
}
