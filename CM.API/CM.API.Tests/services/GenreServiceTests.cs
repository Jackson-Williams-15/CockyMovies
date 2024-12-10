// Import necessary namespaces for the test class
using CM.API.Services;         // Business logic services for genres
using CM.API.Data;             // Data access layer
using CM.API.Repositories;     // Repository for accessing genre data
using CM.API.Models;           // Model definitions for genres
using Microsoft.EntityFrameworkCore; // Entity Framework Core for database management
using Xunit;                   // Testing framework for unit tests
using System;                  // For IDisposable interface
using System.Collections.Generic; // For List<T> collections
using System.Threading.Tasks;  // For async/await in tests

// Test class for GenreService implementing IDisposable for cleanup
public class GenreServiceTests : IDisposable
{
    private readonly GenreService _genreService; // Service under test
    private readonly AppDbContext _context; // In-memory database context
    private readonly GenreRepository _genreRepository; // Repository for genre data access

    // Constructor initializes the in-memory database, repository, and service
    public GenreServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique database for test isolation
            .Options;

        _context = new AppDbContext(options); // Initialize database context
        _genreRepository = new GenreRepository(_context); // Initialize repository
        _genreService = new GenreService(_genreRepository); // Initialize service
    }

    // Cleanup method to delete the database after tests
    public void Dispose() => _context.Database.EnsureDeleted();

    // Test to ensure retrieving existing genres works correctly
    [Fact]
    public async Task GetGenres_ShouldReturnGenres_WhenGenresExist()
    {
        // Arrange: Seed the database with sample genres
        var genres = new List<Genre>
        {
            new Genre { Id = 1, Name = "Action" },
            new Genre { Id = 2, Name = "Comedy" }
        };

        _context.Genres.AddRange(genres); // Add genres to database
        await _context.SaveChangesAsync(); // Save changes

        // Act: Retrieve genres using the service
        var result = await _genreService.GetGenres();

        // Assert: Verify the correct data was returned
        Assert.NotNull(result); // Ensure result is not null
        Assert.Equal(2, result.Count); // Ensure the expected count
        Assert.Contains(result, g => g.Name == "Action"); // Check for "Action"
        Assert.Contains(result, g => g.Name == "Comedy"); // Check for "Comedy"
    }

    // Test to ensure retrieving genres returns an empty list when none exist
    [Fact]
    public async Task GetGenres_ShouldReturnEmptyList_WhenNoGenresExist()
    {
        // Act: Retrieve genres when database is empty
        var result = await _genreService.GetGenres();

        // Assert: Verify result is an empty list
        Assert.NotNull(result); // Ensure result is not null
        Assert.Empty(result); // Ensure the list is empty
    }

    // Test to ensure fetching a genre by valid ID works
    [Fact]
    public async Task GetGenreById_ShouldReturnGenre_WhenGenreExists()
    {
        // Arrange: Add a genre to the database
        var genre = new Genre { Name = "Action" };
        _context.Genres.Add(genre); // Add to context
        await _context.SaveChangesAsync(); // Save changes

        // Act: Fetch genre by its ID
        var result = await _genreService.GetGenreById(genre.Id);

        // Assert: Verify the genre exists and data matches
        Assert.NotNull(result); // Ensure result is not null
        Assert.Equal("Action", result!.Name); // Ensure correct genre name
    }

    // Test to ensure fetching a genre by invalid ID returns null
    [Fact]
    public async Task GetGenreById_ShouldReturnNull_WhenGenreDoesNotExist()
    {
        // Act: Attempt to fetch a genre with an invalid ID
        var result = await _genreService.GetGenreById(999);

        // Assert: Ensure result is null
        Assert.Null(result);
    }

    // Test to ensure adding a new genre succeeds
    [Fact]
    public async Task AddGenre_ShouldReturnTrue_WhenGenreIsAddedSuccessfully()
    {
        // Arrange: Prepare a new genre
        var genre = new Genre { Name = "Horror" };

        // Act: Add the genre using the service
        var result = await _genreService.AddGenre(genre);

        // Assert: Ensure the operation succeeded
        Assert.True(result);

        // Verify the genre was saved in the database
        var savedGenre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == "Horror");
        Assert.NotNull(savedGenre); // Ensure genre was saved
    }

    // Test to ensure adding a duplicate genre fails
    [Fact]
    public async Task AddGenre_ShouldReturnFalse_WhenAddFails()
    {
        // Arrange: Add a genre to the database
        var existingGenre = new Genre { Name = "Sci-Fi" };
        _context.Genres.Add(existingGenre); // Add to context
        await _context.SaveChangesAsync(); // Save changes

        // Prepare a duplicate genre with the same name
        var newGenre = new Genre { Name = "Sci-Fi" };

        // Act: Attempt to add the duplicate genre
        var result = await _genreService.AddGenre(newGenre);

        // Assert: Ensure the operation failed
        Assert.False(result); // Ensure failure

        // Verify only one instance of the genre exists
        var count = await _context.Genres.CountAsync(g => g.Name == "Sci-Fi");
        Assert.Equal(1, count); // Ensure only one genre exists
    }
}
