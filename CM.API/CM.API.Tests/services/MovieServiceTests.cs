using CM.API.Services;  
using CM.API.Data;
using CM.API.Models;
using Moq;              
using CM.API.Repositories;  
using Microsoft.EntityFrameworkCore;  

public class MovieServiceTests
{
    private readonly MovieService _movieService;
    private readonly Mock<GenreRepository> _mockGenreRepository;
    private readonly AppDbContext _context;

    public MovieServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
                        .UseInMemoryDatabase(databaseName: "TestDb")
                        .Options;

        _context = new AppDbContext(options);
        _mockGenreRepository = new Mock<GenreRepository>(_context);
        _movieService = new MovieService(_context, _mockGenreRepository.Object);

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        var movie = new Movie
        {
            Title = "Some Movie Title",
            DateReleased = DateTime.Now,
            RatingId = 1,
            Showtimes = new List<Showtime>
            {
                new Showtime
                {
                    StartTime = DateTime.Now,
                    Capacity = 100,
                    TicketsAvailable = 100,
                    MovieId = 1,
                    Movie = null!, 
                    Tickets = new List<Ticket>
                    {
                        new Ticket
                        {
                            Price = 10.00m,
                            IsSold = false,
                            Showtime = null! 
                        }
                    }
                }
            }
        };

        _context.Movies.Add(movie);
        _context.SaveChanges();
    }

    [Fact]
    public async Task AddMovie_ShouldReturnTrue_WhenMovieIsAddedSuccessfully()
    {
        var movie = new Movie
        {
            Title = "Another Movie Title",
            DateReleased = DateTime.Now,
            RatingId = 1,
            Showtimes = new List<Showtime>
            {
                new Showtime
                {
                    StartTime = DateTime.Now.AddHours(2),
                    Capacity = 200,
                    TicketsAvailable = 200,
                    MovieId = 1,
                    Movie = null!,
                    Tickets = new List<Ticket>
                    {
                        new Ticket
                        {
                            Price = 15.00m,
                            IsSold = false,
                            Showtime = null!
                        }
                    }
                }
            }
        };

        var result = await _movieService.AddMovie(movie);

        Assert.True(result);
        Assert.Equal(2, _context.Movies.Count());
    }

    [Fact]
    public async Task AddMovie_ShouldReturnFalse_WhenMovieAlreadyExists()
    {
        var existingMovie = new Movie
        {
            Id = 1,
            Title = "Test Movie",
            DateReleased = DateTime.Now,
            RatingId = 1,
            Showtimes = new List<Showtime>
            {
                new Showtime
                {
                    StartTime = DateTime.Now.AddHours(1),
                    Capacity = 150,
                    TicketsAvailable = 150,
                    MovieId = 1,
                    Movie = null!,
                    Tickets = new List<Ticket>
                    {
                        new Ticket
                        {
                            Price = 20.00m,
                            IsSold = false,
                            Showtime = null!
                        }
                    }
                }
            }
        };

        var result = await _movieService.AddMovie(existingMovie);

        Assert.False(result);
    }

    [Fact]
    public async Task RemoveMovie_ShouldReturnTrue_WhenMovieIsRemovedSuccessfully()
    {
        var movieToRemove = await _context.Movies.FirstAsync();

        var result = await _movieService.RemoveMovie(movieToRemove);

        Assert.True(result);
        Assert.Empty(_context.Movies);
    }

    [Fact]
    public async Task RemoveMovie_ShouldReturnFalse_WhenMovieDoesNotExist()
    {
        // Arrange
        var nonExistingMovie = new Movie 
        { 
            Id = 999, 
            Title = "Non-Existent",  // Required property
            Showtimes = new List<Showtime>()  // Required property
        };

        // Act
        var result = await _movieService.RemoveMovie(nonExistingMovie);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task EditMovie_ShouldReturnTrue_WhenMovieIsUpdatedSuccessfully()
    {
        var oldMovie = await _context.Movies.FirstAsync();
        var newMovie = new Movie
        {
            Title = "Updated Movie",
            Description = "Updated description",
            DateReleased = DateTime.Now.AddYears(1),
            RatingId = 3,
            Showtimes = oldMovie.Showtimes 
        };

        var result = await _movieService.EditMovie(oldMovie, newMovie);

        Assert.True(result);
        var updatedMovie = await _context.Movies.FirstAsync();
        Assert.Equal("Updated Movie", updatedMovie.Title);
        Assert.Equal("Updated description", updatedMovie.Description);
    }

    [Fact]
    public async Task EditMovie_ShouldReturnFalse_WhenMovieDoesNotExist()
    {
        // Arrange
        var nonExistingMovie = new Movie 
        { 
            Id = 999, 
            Title = "Fake Movie",  // Required property
            Showtimes = new List<Showtime>()  // Required property
        };

        var newMovie = new Movie 
        { 
            Title = "Updated Fake Movie",  // Required property
            Showtimes = new List<Showtime>()  // Required property
        };

        // Act
        var result = await _movieService.EditMovie(nonExistingMovie, newMovie);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetMovieById_ShouldReturnMovie_WhenMovieExists()
    {
        var movie = await _movieService.GetMovieById(1);

        Assert.NotNull(movie);
        Assert.Equal("Some Movie Title", movie.Title);
    }

    [Fact]
    public async Task GetMovieById_ShouldReturnNull_WhenMovieDoesNotExist()
    {
        var movie = await _movieService.GetMovieById(999);

        Assert.Null(movie);
    }

    [Fact]
    public async Task GetMovies_ShouldReturnMovies_WhenMoviesMatchFilters()
    {
        var movies = await _movieService.GetMovies(genreIds: new List<int> { 1 });

        Assert.NotEmpty(movies);
        Assert.Equal("Some Movie Title", movies.First().Title);
    }
}
