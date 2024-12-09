using CM.API.Services;         // For GenreService
using CM.API.Data;             // For AppDbContext
using CM.API.Repositories;     // For GenreRepository
using CM.API.Models;           // For Genre
using Microsoft.EntityFrameworkCore; // For DbContext
using Xunit;                   // For testing framework
using System;                  // For IDisposable
using System.Collections.Generic; // For List<T>
using System.Threading.Tasks;  // For async/await

public class GenreServiceTests : IDisposable
{
    private readonly GenreService _genreService;
    private readonly AppDbContext _context;
    private readonly GenreRepository _genreRepository;

    public GenreServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique DB for isolation
            .Options;

        _context = new AppDbContext(options);
        _genreRepository = new GenreRepository(_context);
        _genreService = new GenreService(_genreRepository);
    }

    public void Dispose() => _context.Database.EnsureDeleted();

    [Fact]
    public async Task GetGenres_ShouldReturnGenres_WhenGenresExist()
    {
        // Arrange
        var genres = new List<Genre>
        {
            new Genre { Id = 1, Name = "Action" },
            new Genre { Id = 2, Name = "Comedy" }
        };

        _context.Genres.AddRange(genres);
        await _context.SaveChangesAsync();

        // Act
        var result = await _genreService.GetGenres();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(result, g => g.Name == "Action");
        Assert.Contains(result, g => g.Name == "Comedy");
    }

    [Fact]
    public async Task GetGenres_ShouldReturnEmptyList_WhenNoGenresExist()
    {
        // Act
        var result = await _genreService.GetGenres();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetGenreById_ShouldReturnGenre_WhenGenreExists()
    {
        // Arrange
        var genre = new Genre { Name = "Action" };
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();

        // Act
        var result = await _genreService.GetGenreById(genre.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Action", result!.Name);
    }

    [Fact]
    public async Task GetGenreById_ShouldReturnNull_WhenGenreDoesNotExist()
    {
        // Act
        var result = await _genreService.GetGenreById(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddGenre_ShouldReturnTrue_WhenGenreIsAddedSuccessfully()
    {
        // Arrange
        var genre = new Genre { Name = "Horror" };

        // Act
        var result = await _genreService.AddGenre(genre);

        // Assert
        Assert.True(result);

        // Reload context to avoid cache issues
        var savedGenre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == "Horror");
        Assert.NotNull(savedGenre);
    }

    [Fact]
    public async Task AddGenre_ShouldReturnFalse_WhenAddFails()
    {
        // Arrange
        var existingGenre = new Genre { Name = "Sci-Fi" };
        _context.Genres.Add(existingGenre);
        await _context.SaveChangesAsync();

        var newGenre = new Genre { Name = "Sci-Fi" };  // Duplicate name

        // Act
        var result = await _genreService.AddGenre(newGenre);

        // Assert
        Assert.False(result);

        // Verify only one genre exists
        var count = await _context.Genres.CountAsync(g => g.Name == "Sci-Fi");
        Assert.Equal(1, count);
    }
}
