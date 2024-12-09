using CM.API.Models;
using CM.API.Services;
using CM.API.Repositories;
using CM.API.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class MovieServiceTests
{
    private readonly MovieService _movieService;
    private readonly Mock<GenreRepository> _mockGenreRepository;
    private readonly AppDbContext _context;

    public MovieServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _mockGenreRepository = new Mock<GenreRepository>();
        _movieService = new MovieService(_context, _mockGenreRepository.Object);

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
        _context.Users.Add(new User
        {
            Id = 1,
            Username = "TestUser",
            Password = "TestPassword",
            Email = "testuser@example.com"
        });

        var genre = new Genre { Id = 1, Name = "Action" };
        _context.Genres.Add(genre);

        var movie = new Movie
        {
            Id = 1,
            Title = "Sample Movie",
            DateReleased = DateTime.Now,
            RatingId = 1,
            Genres = new List<Genre> { genre }
        };

        _context.Movies.Add(movie);
        _context.SaveChanges();
    }

    [Fact]
    public async Task AddMovie_ShouldReturnTrue_WhenMovieIsAddedSuccessfully()
    {
        var movie = new Movie
        {
            Title = "Another Movie",
            DateReleased = DateTime.Now,
            RatingId = 1
        };

        var result = await _movieService.AddMovie(movie);

        Assert.True(result);
        Assert.Equal(2, _context.Movies.Count());
    }
}
