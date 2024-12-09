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
    private readonly Mock<IGenreService> _mockGenreService; // Mock for IGenreService
    private readonly GenreController _controller; // The controller under test

    // Constructor to set up mock services and initialize the controller instance
    public GenreControllerTests()
    {
        _mockGenreService = new Mock<IGenreService>();
        _controller = new GenreController(_mockGenreService.Object); // Initialize controller with mocked GenreService
    }

    // Test for GetAllGenres method when genres are successfully retrieved
    [Fact]
    public async Task GetAllGenres_ReturnsOk_WithGenreList()
    {
        // Arrange: Create a list of genres to return from the mock service
        var genres = new List<Genre>
        {
            new Genre { Id = 1, Name = "Action" },
            new Genre { Id = 2, Name = "Comedy" }
        };
        _mockGenreService.Setup(s => s.GetGenres()).ReturnsAsync(genres); // Mock the GetGenres method

        // Act: Call the controller method
        var result = await _controller.GetAllGenres();

        // Assert: Verify the result is Ok and contains the expected list of genres
        var okResult = Assert.IsType<OkObjectResult>(result); // Expect Ok result
        var returnedGenres = Assert.IsType<List<GenreDto>>(okResult.Value); // Ensure the returned value is a List of GenreDto
        Assert.Equal(2, returnedGenres.Count); // Ensure the number of genres is correct
    }

    // Test for GetGenreById method when the genre is found
    [Fact]
    public async Task GetGenreById_ReturnsOk_WithGenre()
    {
        // Arrange: Create a genre object to return from the mock service
        var genre = new Genre { Id = 1, Name = "Action" };
        _mockGenreService.Setup(s => s.GetGenreById(1)).ReturnsAsync(genre); // Mock the GetGenreById method

        // Act: Call the controller method
        var result = await _controller.GetGenreById(1);

        // Assert: Verify the result is Ok and contains the correct genre
        var okResult = Assert.IsType<OkObjectResult>(result); // Expect Ok result
        var returnedGenre = Assert.IsType<GenreDto>(okResult.Value); // Ensure the returned value is a GenreDto
        Assert.Equal(1, returnedGenre.Id); // Ensure the genre ID is correct
        Assert.Equal("Action", returnedGenre.Name); // Ensure the genre name is correct
    }

    // Test for GetGenreById method when the genre does not exist
    [Fact]
    public async Task GetGenreById_ReturnsNotFound_WhenGenreDoesNotExist()
    {
        // Arrange: Mock the service to return null when a genre with ID 1 is requested
        _mockGenreService.Setup(s => s.GetGenreById(1)).ReturnsAsync((Genre)null); // Genre does not exist

        // Act: Call the controller method
        var result = await _controller.GetGenreById(1);

        // Assert: Verify the result is NotFound since the genre does not exist
        Assert.IsType<NotFoundResult>(result); // Expect NotFound result
    }

    // Test for AddGenre method when a genre is successfully added
    [Fact]
    public async Task AddGenre_ReturnsOk_WhenGenreIsAddedSuccessfully()
    {
        // Arrange: Create a GenreCreateDto to add
        var genreDto = new GenreCreateDto { Name = "Action" };
        _mockGenreService.Setup(s => s.AddGenre(It.IsAny<Genre>())).ReturnsAsync(true); // Mock AddGenre to return true (successful addition)

        // Act: Call the controller method
        var result = await _controller.AddGenre(genreDto);

        // Assert: Verify the result is Ok and contains the success message
        var okResult = Assert.IsType<OkObjectResult>(result); // Expect Ok result
        Assert.Equal("Genre added successfully.", okResult.Value); // Ensure the message is correct
    }

    // Test for AddGenre method when the genre already exists
    [Fact]
    public async Task AddGenre_ReturnsBadRequest_WhenGenreAlreadyExists()
    {
        // Arrange: Create a GenreCreateDto with a name that already exists
        var genreDto = new GenreCreateDto { Name = "Action" };
        _mockGenreService.Setup(s => s.AddGenre(It.IsAny<Genre>())).ReturnsAsync(false); // Mock AddGenre to return false (genre exists)

        // Act: Call the controller method
        var result = await _controller.AddGenre(genreDto);

        // Assert: Verify the result is BadRequest with the appropriate error message
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result); // Expect BadRequest result
        Assert.Equal("A genre with the same name already exists.", badRequestResult.Value); // Ensure the error message is correct
    }
}
