using CM.API.Controllers;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class MoviesControllerTests
{
    private readonly Mock<IMovieService> _mockMovieService;
    private readonly MoviesController _controller;

    public MoviesControllerTests()
    {
        _mockMovieService = new Mock<IMovieService>();
        _controller = new MoviesController(_mockMovieService.Object);
    }

    [Fact]
    public async Task GetMovies_ReturnsOk_WithMovieList()
    {
        // Arrange
        var movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "Movie 1", Description = "Description 1", DateReleased = System.DateTime.Now, Showtimes = new List<Showtime>(), Genres = new List<Genre>(), Rating = new Rating { Id = 1, Name = "PG-13" }, ImageUrl = "http://example.com/image1.jpg" },
            new Movie { Id = 2, Title = "Movie 2", Description = "Description 2", DateReleased = System.DateTime.Now, Showtimes = new List<Showtime>(), Genres = new List<Genre>(), Rating = new Rating { Id = 2, Name = "R" }, ImageUrl = "http://example.com/image2.jpg" }
        };
        _mockMovieService.Setup(s => s.GetMovies(It.IsAny<List<int>>(), It.IsAny<List<int>>())).ReturnsAsync(movies);

        // Act
        var result = await _controller.GetMovies();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedMovies = Assert.IsType<List<MovieDto>>(okResult.Value);
        Assert.Equal(2, returnedMovies.Count);
    }

    [Fact]
    public async Task GetMovieById_ReturnsOk_WithMovie()
    {
        // Arrange
        var movie = new Movie
        {
            Id = 1,
            Title = "Movie 1",
            Description = "Description 1",
            DateReleased = System.DateTime.Now,
            Genres = new List<Genre> { new Genre { Id = 1, Name = "Action" } },
            Showtimes = new List<Showtime> { new Showtime { Id = 1, StartTime = System.DateTime.Now, Tickets = new List<Ticket>(), Movie = new Movie { Id = 1, Title = "Movie 1", Showtimes = new List<Showtime>(), Genres = new List<Genre>(), Rating = new Rating { Id = 1, Name = "PG-13" }, ImageUrl = "http://example.com/image1.jpg" } } },
            ImageUrl = "http://example.com/image.jpg",
            Rating = new Rating { Id = 1, Name = "PG-13" }
        };
        _mockMovieService.Setup(s => s.GetMovieById(1)).ReturnsAsync(movie);

        // Act
        var result = await _controller.GetMovieById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedMovie = Assert.IsType<MovieDto>(okResult.Value);
        Assert.Equal(1, returnedMovie.Id);
        Assert.Equal("Movie 1", returnedMovie.Title);
    }

    [Fact]
    public async Task GetMovieById_ReturnsNotFound_WhenMovieDoesNotExist()
    {
        // Arrange
        _mockMovieService.Setup(s => s.GetMovieById(1)).ReturnsAsync((Movie)null);

        // Act
        var result = await _controller.GetMovieById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task AddMovie_ReturnsOk_WhenMovieIsAddedSuccessfully()
    {
        // Arrange
        var movieDto = new MovieCreateDto
        {
            Title = "New Movie",
            Description = "New Description",
            DateReleased = System.DateTime.Now,
            GenreIds = new List<int> { 1 },
            RatingId = 1,
            ImageUrl = "http://example.com/image.jpg"
        };
        _mockMovieService.Setup(s => s.GetGenresByIds(movieDto.GenreIds)).ReturnsAsync(new List<Genre> { new Genre { Id = 1, Name = "Action" } });
        _mockMovieService.Setup(s => s.AddMovie(It.IsAny<Movie>())).ReturnsAsync(true);

        // Act
        var result = await _controller.AddMovie(movieDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Movie added successfully.", okResult.Value);
    }

    [Fact]
    public async Task AddMovie_ReturnsBadRequest_WhenGenreIdsAreInvalid()
    {
        // Arrange
        var movieDto = new MovieCreateDto
        {
            Title = "New Movie",
            Description = "New Description",
            DateReleased = System.DateTime.Now,
            GenreIds = new List<int> { 1 },
            RatingId = 1,
            ImageUrl = "http://example.com/image.jpg"
        };
        _mockMovieService.Setup(s => s.GetGenresByIds(movieDto.GenreIds)).ReturnsAsync(new List<Genre>());

        // Act
        var result = await _controller.AddMovie(movieDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid genre IDs provided.", badRequestResult.Value);
    }

    [Fact]
    public async Task RemoveMovie_ReturnsOk_WhenMovieIsRemovedSuccessfully()
    {
        // Arrange
        var movie = new Movie { Id = 1, Title = "Movie 1", Showtimes = new List<Showtime>(), Genres = new List<Genre>(), Rating = new Rating { Id = 1, Name = "PG-13" }, ImageUrl = "http://example.com/image1.jpg" };
        _mockMovieService.Setup(s => s.GetMovieById(1)).ReturnsAsync(movie);
        _mockMovieService.Setup(s => s.RemoveMovie(movie)).ReturnsAsync(true);

        // Act
        var result = await _controller.RemoveMovie(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Movie removed successfully.", okResult.Value);
    }

    [Fact]
    public async Task RemoveMovie_ReturnsNotFound_WhenMovieDoesNotExist()
    {
        // Arrange
        _mockMovieService.Setup(s => s.GetMovieById(1)).ReturnsAsync((Movie)null);

        // Act
        var result = await _controller.RemoveMovie(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
