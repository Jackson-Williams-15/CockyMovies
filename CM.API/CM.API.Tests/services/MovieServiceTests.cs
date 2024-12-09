using CM.API.Models;
using CM.API.Services;
using CM.API.Repositories;
using CM.API.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

public class MovieServiceTests
{
    private readonly MovieService _movieService;
    private readonly AppDbContext _context;
    private readonly GenreRepository _genreRepository;

    public MovieServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())  // Unique In-Memory Database
            .Options;

        _context = new AppDbContext(options);
        _genreRepository = new GenreRepository(_context);
        _movieService = new MovieService(_context, _genreRepository);
    }

    private void ResetDatabase()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

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
            Genres = new List<Genre> { genre }
        };

        _context.Users.Add(user);
        _context.Genres.Add(genre);
        _context.Movies.Add(movie);
        _context.SaveChanges();
    }

    [Fact]
    public async Task AddMovie_ShouldReturnTrue_WhenMovieIsAddedSuccessfully()
    {
        // Arrange
        ResetDatabase();
        SeedDatabase();

        var newMovie = new Movie
        {
            Id = Guid.NewGuid().GetHashCode(),
            Title = "Another Movie",
            DateReleased = DateTime.Now,
            RatingId = Guid.NewGuid().GetHashCode()
        };

        // Act
        var result = await _movieService.AddMovie(newMovie);

        // Assert
        Assert.True(result);

        // Ensure the movie with the exact title was added
        var movieCount = _context.Movies.Count(m => m.Title == "Another Movie");
        Assert.Equal(1, movieCount);
    }

    [Fact]
    public async Task AddMovie_ShouldFail_WhenMovieAlreadyExists()
    {
        // Arrange
        ResetDatabase();
        SeedDatabase();
        var existingMovie = _context.Movies.First();

        // Act
        var result = await _movieService.AddMovie(existingMovie);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetMovieById_ShouldReturnMovie_WhenValidId()
    {
        // Arrange
        ResetDatabase();
        SeedDatabase();
        var movie = _context.Movies.First();

        // Act
        var result = await _movieService.GetMovieById(movie.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(movie.Title, result.Title);
    }

    [Fact]
    public async Task GetMovieById_ShouldReturnNull_WhenInvalidId()
    {
        // Arrange
        ResetDatabase();

        // Act
        var result = await _movieService.GetMovieById(Guid.NewGuid().GetHashCode());

        // Assert
        Assert.Null(result);
    }
}
