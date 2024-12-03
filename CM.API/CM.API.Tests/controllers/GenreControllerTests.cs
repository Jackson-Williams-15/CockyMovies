using CM.API.Controllers;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class GenreControllerTests
{
    private readonly Mock<IGenreService> _mockGenreService;
    private readonly GenreController _controller;

    public GenreControllerTests()
    {
        _mockGenreService = new Mock<IGenreService>();
        _controller = new GenreController(_mockGenreService.Object);
    }

    [Fact]
    public async Task GetAllGenres_ReturnsOk_WithGenreList()
    {
        // Arrange
        var genres = new List<Genre>
        {
            new Genre { Id = 1, Name = "Action" },
            new Genre { Id = 2, Name = "Comedy" }
        };
        _mockGenreService.Setup(s => s.GetGenres()).ReturnsAsync(genres);

        // Act
        var result = await _controller.GetAllGenres();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedGenres = Assert.IsType<List<GenreDto>>(okResult.Value);
        Assert.Equal(2, returnedGenres.Count);
    }

    [Fact]
    public async Task GetGenreById_ReturnsOk_WithGenre()
    {
        // Arrange
        var genre = new Genre { Id = 1, Name = "Action" };
        _mockGenreService.Setup(s => s.GetGenreById(1)).ReturnsAsync(genre);

        // Act
        var result = await _controller.GetGenreById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedGenre = Assert.IsType<GenreDto>(okResult.Value);
        Assert.Equal(1, returnedGenre.Id);
        Assert.Equal("Action", returnedGenre.Name);
    }

    [Fact]
    public async Task GetGenreById_ReturnsNotFound_WhenGenreDoesNotExist()
    {
        // Arrange
        _mockGenreService.Setup(s => s.GetGenreById(1)).ReturnsAsync((Genre)null);

        // Act
        var result = await _controller.GetGenreById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task AddGenre_ReturnsOk_WhenGenreIsAddedSuccessfully()
    {
        // Arrange
        var genreDto = new GenreCreateDto { Name = "Action" };
        _mockGenreService.Setup(s => s.AddGenre(It.IsAny<Genre>())).ReturnsAsync(true);

        // Act
        var result = await _controller.AddGenre(genreDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Genre added successfully.", okResult.Value);
    }

    [Fact]
    public async Task AddGenre_ReturnsBadRequest_WhenGenreAlreadyExists()
    {
        // Arrange
        var genreDto = new GenreCreateDto { Name = "Action" };
        _mockGenreService.Setup(s => s.AddGenre(It.IsAny<Genre>())).ReturnsAsync(false);

        // Act
        var result = await _controller.AddGenre(genreDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("A genre with the same name already exists.", badRequestResult.Value);
    }
}
