// Import necessary namespaces for the test class
using CM.API.Models; // Models representing database entities
using CM.API.Services; // Business logic services
using CM.API.Repositories; // Repository for accessing data
using CM.API.Data; // Data access layer
using Microsoft.EntityFrameworkCore; // Entity Framework Core for in-memory database
using Xunit; // Testing framework
using System; // For DateTime and GUIDs
using System.Collections.Generic; // For lists and collections
using System.Threading.Tasks; // For async methods
using System.Linq; // For LINQ queries

// Test class for MovieService
public class MovieServiceTests
{
    private readonly MovieService _movieService; // Service being tested
    private readonly AppDbContext _context; // In-memory database context
    private readonly GenreRepository _genreRepository; // Genre repository for dependency injection

    // Constructor initializes in-memory database, repositories, and service
    public MovieServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique in-memory database per test
            .Options;

        _context = new AppDbContext(options); // Initialize database context
        _genreRepository = new GenreRepository(_context); // Initialize genre repository
        _movieService = new MovieService(_context, _genreRepository); // Create service instance
    }

    // Resets the database state before each test
    private void ResetDatabase()
    {
        _context.Database.EnsureDeleted(); // Delete database
        _context.Database.EnsureCreated(); // Recreate database
    }

    // Seeds the database with initial test data
    private void SeedDatabase()
    {
        var user = new User
        {
            Id = Guid.NewGuid().GetHashCode(),
            Username = "TestUser",
            Password = "TestPassword",
            Email = "testuser@example.com"
        };

        var genre = new Genre
        {
            Id = Guid.NewGuid().GetHashCode(),
            Name = "Action"
        };

        var movie = new Movie
        {
            Id = Guid.NewGuid().GetHashCode(),
            Title = "Sample Movie",
            DateReleased = DateTime.Now,
            RatingId = Guid.NewGuid().GetHashCode(),
            Genres = new List<Genre> { genre } // Assign genre to movie
        };

        // Add initial entities to context
        _context.Users.Add(user);
        _context.Genres.Add(genre);
        _context.Movies.Add(movie);
        _context.SaveChanges(); // Save changes
    }

    // Test to ensure adding a new movie succeeds
    [Fact]
    public async Task AddMovie_ShouldReturnTrue_WhenMovieIsAddedSuccessfully()
    {
        // Arrange: Reset and seed database
        ResetDatabase();
        SeedDatabase();

        var newMovie = new Movie
        {
            Id = Guid.NewGuid().GetHashCode(),
            Title = "Another Movie",
            DateReleased = DateTime.Now,
            RatingId = Guid.NewGuid().GetHashCode()
        };

        // Act: Call service to add movie
        var result = await _movieService.AddMovie(newMovie);

        // Assert: Ensure movie was added successfully
        Assert.True(result);

        // Verify the exact movie was added
        var movieCount = _context.Movies.Count(m => m.Title == "Another Movie");
        Assert.Equal(1, movieCount);
    }

    // Test to ensure adding a duplicate movie fails
    [Fact]
    public async Task AddMovie_ShouldFail_WhenMovieAlreadyExists()
    {
        // Arrange: Reset and seed database
        ResetDatabase();
        SeedDatabase();
        var existingMovie = _context.Movies.First(); // Get first movie from database

        // Act: Attempt to add duplicate movie
        var result = await _movieService.AddMovie(existingMovie);

        // Assert: Ensure operation failed
        Assert.False(result);
    }

    // Test to ensure fetching a movie by valid ID succeeds
    [Fact]
    public async Task GetMovieById_ShouldReturnMovie_WhenValidId()
    {
        // Arrange: Reset and seed database
        ResetDatabase();
        SeedDatabase();
        var movie = _context.Movies.First(); // Get first movie from database

        // Act: Fetch movie by ID
        var result = await _movieService.GetMovieById(movie.Id);

        // Assert: Ensure movie was found and data matches
        Assert.NotNull(result);
        Assert.Equal(movie.Title, result.Title);
    }

    // Test to ensure fetching a movie by invalid ID returns null
    [Fact]
    public async Task GetMovieById_ShouldReturnNull_WhenInvalidId()
    {
        // Arrange: Reset database without seeding
        ResetDatabase();

        // Act: Attempt to fetch movie by invalid ID
        var result = await _movieService.GetMovieById(Guid.NewGuid().GetHashCode());

        // Assert: Ensure result is null
        Assert.Null(result);
    }
}
