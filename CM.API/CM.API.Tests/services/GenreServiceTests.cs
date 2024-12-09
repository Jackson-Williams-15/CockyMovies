using CM.API.Models;
using CM.API.Repositories;
using CM.API.Services;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GenreServiceTests
{
    private readonly GenreService _genreService;
    private readonly Mock<GenreRepository> _mockGenreRepository;

    public GenreServiceTests()
    {
        _mockGenreRepository = new Mock<GenreRepository>();
        _genreService = new GenreService(_mockGenreRepository.Object);
    }

    [Fact]
    public async Task GetGenres_ShouldReturnGenres_WhenGenresExist()
    {
        // Arrange
        var genres = new List<Genre>
        {
            new Genre { Id = 1, Name = "Action" },
            new Genre { Id = 2, Name = "Comedy" }
        };
        
        _mockGenreRepository.Setup(repo => repo.GetGenres())
            .ReturnsAsync(genres);

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
        // Arrange
        _mockGenreRepository.Setup(repo => repo.GetGenres())
            .ReturnsAsync(new List<Genre>());

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
        var genre = new Genre { Id = 1, Name = "Action" };
        
        _mockGenreRepository.Setup(repo => repo.GetGenreById(1))
            .ReturnsAsync(genre);

        // Act
        var result = await _genreService.GetGenreById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Action", result.Name);
    }

    [Fact]
    public async Task GetGenreById_ShouldReturnNull_WhenGenreDoesNotExist()
    {
        // Arrange
        _mockGenreRepository.Setup(repo => repo.GetGenreById(It.IsAny<int>()))
            .ReturnsAsync((Genre?)null);

        // Act
        var result = await _genreService.GetGenreById(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddGenre_ShouldReturnTrue_WhenGenreIsAddedSuccessfully()
    {
        // Arrange
        var genre = new Genre { Id = 3, Name = "Horror" };

        _mockGenreRepository.Setup(repo => repo.AddGenre(genre))
            .ReturnsAsync(true);

        // Act
        var result = await _genreService.AddGenre(genre);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task AddGenre_ShouldReturnFalse_WhenAddFails()
    {
        // Arrange
        var genre = new Genre { Id = 4, Name = "Sci-Fi" };

        _mockGenreRepository.Setup(repo => repo.AddGenre(genre))
            .ReturnsAsync(false);

        // Act
        var result = await _genreService.AddGenre(genre);

        // Assert
        Assert.False(result);
    }
}
