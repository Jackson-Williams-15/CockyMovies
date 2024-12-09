using CM.API.Controllers;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class ShowtimesControllerTests
{
    private readonly Mock<IShowtimeService> _mockShowtimeService;  // Mock for ShowtimeService
    private readonly Mock<IMovieService> _mockMovieService;  // Mock for MovieService
    private readonly Mock<ILogger<ShowtimesController>> _mockLogger;  // Mock for ILogger
    private readonly ShowtimesController _controller;  // The controller under test

    // Constructor to set up mock services and controller instance
    public ShowtimesControllerTests()
    {
        _mockShowtimeService = new Mock<IShowtimeService>();  // Initialize the mock for ShowtimeService
        _mockMovieService = new Mock<IMovieService>();  // Initialize the mock for MovieService
        _mockLogger = new Mock<ILogger<ShowtimesController>>();  // Initialize the mock logger
        _controller = new ShowtimesController(_mockShowtimeService.Object, _mockMovieService.Object, null, _mockLogger.Object);  // Initialize the controller with the mocked services
    }

    // Test for AddShowtime when the showtime is added successfully
    [Fact]
    public async Task AddShowtime_ReturnsOk_WhenShowtimeIsAddedSuccessfully()
    {
        // Arrange: Create a ShowtimeCreateDto to add
        var showtimeDto = new ShowtimeCreateDto
        {
            MovieId = 1,
            StartTime = System.DateTime.Now.AddHours(2),
            Capacity = 100
        };

        // Mock GetMovieById to return a valid movie
        var movie = new Movie { Id = 1, Title = "Test Movie", Showtimes = new List<Showtime>() };
        _mockMovieService.Setup(s => s.GetMovieById(showtimeDto.MovieId)).ReturnsAsync(movie);
        
        // Mock AddShowtime to return true (indicating successful addition)
        _mockShowtimeService.Setup(s => s.AddShowtime(It.IsAny<Showtime>())).ReturnsAsync(true);

        // Act: Call the controller method
        var result = await _controller.AddShowtime(showtimeDto);

        // Assert: Verify that the result is Ok and contains the success message
        var okResult = Assert.IsType<OkObjectResult>(result);  // Expect Ok result
        Assert.Equal("Showtime added successfully.", okResult.Value);  // Ensure the message is correct
    }

    // Test for AddShowtime when the movie is not found
    [Fact]
    public async Task AddShowtime_ReturnsNotFound_WhenMovieIsNotFound()
    {
        // Arrange: Create a ShowtimeCreateDto for a non-existent movie
        var showtimeDto = new ShowtimeCreateDto
        {
            MovieId = 99,  // Invalid movie ID
            StartTime = System.DateTime.Now.AddHours(2),
            Capacity = 100
        };

        // Mock GetMovieById to return null (movie not found)
        _mockMovieService.Setup(s => s.GetMovieById(showtimeDto.MovieId)).ReturnsAsync((Movie)null);

        // Act: Call the controller method
        var result = await _controller.AddShowtime(showtimeDto);

        // Assert: Verify that the result is NotFound and contains the error message
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);  // Expect NotFound result
        Assert.Equal("Movie not found.", notFoundResult.Value);  // Ensure the message is correct
    }

    // Test for GetShowtimesByMovieId when showtimes are found for a movie
    [Fact]
    public async Task GetShowtimesByMovieId_ReturnsOk_WithShowtimesList()
    {
        // Arrange: Create a list of showtimes to return for a movie
        int movieId = 1;
        var showtimes = new List<ShowtimeDto>
        {
            new ShowtimeDto { Id = 1, StartTime = System.DateTime.Now.AddHours(1), AvailableTickets = 100 },
            new ShowtimeDto { Id = 2, StartTime = System.DateTime.Now.AddHours(3), AvailableTickets = 100 }
        };

        // Mock GetShowtimesByMovieId to return the list of showtimes for the specified movie
        _mockShowtimeService.Setup(s => s.GetShowtimesByMovieId(movieId)).ReturnsAsync(showtimes);

        // Act: Call the controller method
        var result = await _controller.GetShowtimesByMovieId(movieId);

        // Assert: Verify the result is Ok and contains the expected list of showtimes
        var okResult = Assert.IsType<OkObjectResult>(result);  // Expect Ok result
        var returnedShowtimes = okResult.Value as List<ShowtimeDto>;  // Ensure the returned value is a List of ShowtimeDto

        Assert.NotNull(returnedShowtimes);  // Ensure the list is not null
        Assert.Equal(2, returnedShowtimes.Count);  // Ensure the correct number of showtimes is returned
    }

    // Test for GetShowtimesByMovieId when no showtimes exist for a movie
    [Fact]
    public async Task GetShowtimesByMovieId_ReturnsNotFound_WhenNoShowtimesExist()
    {
        // Arrange: Mock GetShowtimesByMovieId to return an empty list for the specified movie
        int movieId = 1;
        _mockShowtimeService.Setup(s => s.GetShowtimesByMovieId(movieId)).ReturnsAsync(new List<ShowtimeDto>());  // No showtimes exist

        // Act: Call the controller method
        var result = await _controller.GetShowtimesByMovieId(movieId);

        // Assert: Verify the result is NotFound since no showtimes exist for the movie
        Assert.IsType<NotFoundObjectResult>(result);  // Expect NotFound result
    }
}
