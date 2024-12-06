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
    private readonly Mock<IShowtimeService> _mockShowtimeService;
    private readonly Mock<IMovieService> _mockMovieService;
    private readonly Mock<ILogger<ShowtimesController>> _mockLogger;
    private readonly ShowtimesController _controller;

    public ShowtimesControllerTests()
    {
        _mockShowtimeService = new Mock<IShowtimeService>();
        _mockMovieService = new Mock<IMovieService>();
        _mockLogger = new Mock<ILogger<ShowtimesController>>();
        _controller = new ShowtimesController(_mockShowtimeService.Object, _mockMovieService.Object, null, _mockLogger.Object);
    }

    [Fact]
    public async Task AddShowtime_ReturnsOk_WhenShowtimeIsAddedSuccessfully()
    {
        // Arrange
        var showtimeDto = new ShowtimeCreateDto
        {
            MovieId = 1,
            StartTime = System.DateTime.Now.AddHours(2),
            Capacity = 100
        };

        var movie = new Movie { Id = 1, Title = "Test Movie", Showtimes = new List<Showtime>() };
        _mockMovieService.Setup(s => s.GetMovieById(showtimeDto.MovieId)).ReturnsAsync(movie);
        _mockShowtimeService.Setup(s => s.AddShowtime(It.IsAny<Showtime>())).ReturnsAsync(true);

        // Act
        var result = await _controller.AddShowtime(showtimeDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Showtime added successfully.", okResult.Value);
    }

    [Fact]
    public async Task AddShowtime_ReturnsNotFound_WhenMovieIsNotFound()
    {
        // Arrange
        var showtimeDto = new ShowtimeCreateDto
        {
            MovieId = 99,
            StartTime = System.DateTime.Now.AddHours(2),
            Capacity = 100
        };

        _mockMovieService.Setup(s => s.GetMovieById(showtimeDto.MovieId)).ReturnsAsync((Movie)null);

        // Act
        var result = await _controller.AddShowtime(showtimeDto);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Movie not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task GetShowtimesByMovieId_ReturnsOk_WithShowtimesList()
    {
        // Arrange
        int movieId = 1;
        var showtimes = new List<ShowtimeDto>
        {
            new ShowtimeDto { Id = 1, StartTime = System.DateTime.Now.AddHours(1), AvailableTickets = 100 },
            new ShowtimeDto { Id = 2, StartTime = System.DateTime.Now.AddHours(3), AvailableTickets = 100 }
        };

        _mockShowtimeService.Setup(s => s.GetShowtimesByMovieId(movieId)).ReturnsAsync(showtimes);

        // Act
        var result = await _controller.GetShowtimesByMovieId(movieId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedShowtimes = okResult.Value as List<ShowtimeDto>;

        Assert.NotNull(returnedShowtimes);
        Assert.Equal(2, returnedShowtimes.Count);
    }

    [Fact]
    public async Task GetShowtimesByMovieId_ReturnsNotFound_WhenNoShowtimesExist()
    {
        // Arrange
        int movieId = 1;
        _mockShowtimeService.Setup(s => s.GetShowtimesByMovieId(movieId)).ReturnsAsync(new List<ShowtimeDto>());

        // Act
        var result = await _controller.GetShowtimesByMovieId(movieId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

  
}
