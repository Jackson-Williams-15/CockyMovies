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
    private readonly Mock<IMovieService> _mockMovieService;  // Mock for IMovieService
    private readonly MoviesController _controller;  // The controller under test

    // Constructor to set up mock service and controller instance
    public MoviesControllerTests()
    {
        _mockMovieService = new Mock<IMovieService>();
        _controller = new MoviesController(_mockMovieService.Object);  // Initialize controller with mocked MovieService
    }

    // Test for GetMovies method when movies are successfully retrieved
    [Fact]
    public async Task GetMovies_ReturnsOk_WithMovieList()
    {
        // Arrange: Create a list of movies to return from the mock service
        var movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "Movie 1", Description = "Description 1", DateReleased = System.DateTime.Now, Showtimes = new List<Showtime>(), Genres = new List<Genre>(), Rating = new Rating { Id = 1, Name = "PG-13" }, ImageUrl = "http://example.com/image1.jpg" },
            new Movie { Id = 2, Title = "Movie 2", Description = "Description 2", DateReleased = System.DateTime.Now, Showtimes = new List<Showtime>(), Genres = new List<Genre>(), Rating = new Rating { Id = 2, Name = "R" }, ImageUrl = "http://example.com/image2.jpg" }
        };
        _mockMovieService.Setup(s => s.GetMovies(It.IsAny<List<int>>(), It.IsAny<List<int>>())).ReturnsAsync(movies); // Mock GetMovies method

        // Act: Call the controller method
        var result = await _controller.GetMovies();

        // Assert: Verify the result is Ok and contains the expected list of movies
        var okResult = Assert.IsType<OkObjectResult>(result);  // Expect Ok result
        var returnedMovies = Assert.IsType<List<MovieDto>>(okResult.Value); // Ensure the returned value is a List of MovieDto
        Assert.Equal(2, returnedMovies.Count); // Ensure the number of movies is correct
    }

    // Test for GetMovieById method when the movie is found
    [Fact]
    public async Task GetMovieById_ReturnsOk_WithMovie()
    {
        // Arrange: Create a movie object to return from the mock service
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
        _mockMovieService.Setup(s => s.GetMovieById(1)).ReturnsAsync(movie); // Mock GetMovieById method

        // Act: Call the controller method
        var result = await _controller.GetMovieById(1);

        // Assert: Verify the result is Ok and contains the correct movie
        var okResult = Assert.IsType<OkObjectResult>(result);  // Expect Ok result
        var returnedMovie = Assert.IsType<MovieDto>(okResult.Value); // Ensure the returned value is a MovieDto
        Assert.Equal(1, returnedMovie.Id); // Ensure the movie ID is correct
        Assert.Equal("Movie 1", returnedMovie.Title); // Ensure the movie title is correct
    }

    // Test for GetMovieById method when the movie does not exist
    [Fact]
    public async Task GetMovieById_ReturnsNotFound_WhenMovieDoesNotExist()
    {
        // Arrange: Mock the service to return null when a movie with ID 1 is requested
        _mockMovieService.Setup(s => s.GetMovieById(1)).ReturnsAsync((Movie)null); // Movie does not exist

        // Act: Call the controller method
        var result = await _controller.GetMovieById(1);

        // Assert: Verify the result is NotFound since the movie does not exist
        Assert.IsType<NotFoundResult>(result);  // Expect NotFound result
    }

    // Test for AddMovie method when a movie is successfully added
    [Fact]
    public async Task AddMovie_ReturnsOk_WhenMovieIsAddedSuccessfully()
    {
        // Arrange: Create a MovieCreateDto to add
        var movieDto = new MovieCreateDto
        {
            Title = "New Movie",
            Description = "New Description",
            DateReleased = System.DateTime.Now,
            GenreIds = new List<int> { 1 },
            RatingId = 1,
            ImageUrl = "http://example.com/image.jpg"
        };
        _mockMovieService.Setup(s => s.GetGenresByIds(movieDto.GenreIds)).ReturnsAsync(new List<Genre> { new Genre { Id = 1, Name = "Action" } }); // Mock GetGenresByIds
        _mockMovieService.Setup(s => s.AddMovie(It.IsAny<Movie>())).ReturnsAsync(true); // Mock AddMovie to return true (successful addition)

        // Act: Call the controller method
        var result = await _controller.AddMovie(movieDto);

        // Assert: Verify the result is Ok and contains the success message
        var okResult = Assert.IsType<OkObjectResult>(result); // Expect Ok result
        Assert.Equal("Movie added successfully.", okResult.Value); // Ensure the message is correct
    }

    // Test for AddMovie method when the genre IDs are invalid
    [Fact]
    public async Task AddMovie_ReturnsBadRequest_WhenGenreIdsAreInvalid()
    {
        // Arrange: Create a MovieCreateDto with genre IDs that are invalid (empty list)
        var movieDto = new MovieCreateDto
        {
            Title = "New Movie",
            Description = "New Description",
            DateReleased = System.DateTime.Now,
            GenreIds = new List<int> { 1 },
            RatingId = 1,
            ImageUrl = "http://example.com/image.jpg"
        };
        _mockMovieService.Setup(s => s.GetGenresByIds(movieDto.GenreIds)).ReturnsAsync(new List<Genre>()); // Mock GetGenresByIds to return an empty list

        // Act: Call the controller method
        var result = await _controller.AddMovie(movieDto);

        // Assert: Verify the result is BadRequest with the appropriate error message
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result); // Expect BadRequest result
        Assert.Equal("Invalid genre IDs provided.", badRequestResult.Value); // Ensure the error message is correct
    }

    // Test for RemoveMovie method when movie is successfully removed
    [Fact]
    public async Task RemoveMovie_ReturnsOk_WhenMovieIsRemovedSuccessfully()
    {
        // Arrange: Create a movie object to remove
        var movie = new Movie { Id = 1, Title = "Movie 1", Showtimes = new List<Showtime>(), Genres = new List<Genre>(), Rating = new Rating { Id = 1, Name = "PG-13" }, ImageUrl = "http://example.com/image1.jpg" };
        _mockMovieService.Setup(s => s.GetMovieById(1)).ReturnsAsync(movie);  // Mock GetMovieById method
        _mockMovieService.Setup(s => s.RemoveMovie(movie)).ReturnsAsync(true);  // Mock RemoveMovie to return true (successful removal)

        // Act: Call the controller method
        var result = await _controller.RemoveMovie(1);

        // Assert: Verify the result is Ok and contains the success message
        var okResult = Assert.IsType<OkObjectResult>(result); // Expect Ok result
        Assert.Equal("Movie removed successfully.", okResult.Value); // Ensure the message is correct
    }

    // Test for RemoveMovie method when the movie does not exist
    [Fact]
    public async Task RemoveMovie_ReturnsNotFound_WhenMovieDoesNotExist()
    {
        // Arrange: Mock the service to return null when a movie with ID 1 is requested
        _mockMovieService.Setup(s => s.GetMovieById(1)).ReturnsAsync((Movie)null); // Movie does not exist

        // Act: Call the controller method
        var result = await _controller.RemoveMovie(1);

        // Assert: Verify the result is NotFound since the movie does not exist
        Assert.IsType<NotFoundObjectResult>(result);  // Expect NotFound result
    }
}
