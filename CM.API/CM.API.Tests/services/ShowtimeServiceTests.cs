using CM.API.Models;
using CM.API.Services;
using CM.API.Data;
using CM.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ShowtimeServiceTests
{
    private readonly ShowtimeService _showtimeService;
    private readonly AppDbContext _context;
    private readonly Mock<IMovieService> _mockMovieService;

    public ShowtimeServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new AppDbContext(options);
        _mockMovieService = new Mock<IMovieService>();
        _showtimeService = new ShowtimeService(_context, _mockMovieService.Object);

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        var movie = new Movie
        {
            Id = 1,
            Title = "Test Movie",
            DateReleased = DateTime.Now,
            RatingId = 1,
            Showtimes = new List<Showtime>() // Fix: Initialize required property
        };

        _context.Movies.Add(movie);
        _context.SaveChanges();
    }

    [Fact]
    public async Task AddShowtime_ShouldAddShowtime_WhenValid()
    {
        // Arrange
        var showtime = new Showtime
        {
            Id = 1,
            StartTime = DateTime.Now.AddHours(3),
            Capacity = 100,
            MovieId = 1,
            Movie = new Movie
            {
                Id = 1,
                Title = "Test Movie",
                DateReleased = DateTime.Now,
                RatingId = 1,
                Showtimes = new List<Showtime>()
            },
            Tickets = new List<Ticket>()
        };

        _mockMovieService.Setup(m => m.GetMovieById(1))
            .ReturnsAsync(showtime.Movie);

        // Act
        var result = await _showtimeService.AddShowtime(showtime);

        // Assert
        Assert.True(result);
        Assert.Equal(1, _context.Showtime.Count());
        Assert.Equal(100, showtime.Tickets.Count);
    }

    [Fact]
    public async Task AddShowtime_ShouldFail_WhenShowtimeAlreadyExists()
    {
        // Arrange
        var showtime = new Showtime
        {
            Id = 1,
            StartTime = DateTime.Now.AddHours(3),
            Capacity = 100,
            MovieId = 1,
            Movie = new Movie
            {
                Id = 1,
                Title = "Test Movie",
                DateReleased = DateTime.Now,
                RatingId = 1,
                Showtimes = new List<Showtime>()
            },
            Tickets = new List<Ticket>()
        };

        _context.Showtime.Add(showtime);
        _context.SaveChanges();

        // Act
        var result = await _showtimeService.AddShowtime(showtime);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task RemoveShowtime_ShouldRemoveShowtime_WhenValid()
    {
        // Arrange
        var showtime = new Showtime
        {
            Id = 2,
            StartTime = DateTime.Now.AddHours(4),
            Capacity = 100,
            MovieId = 1,
            Movie = new Movie
            {
                Id = 1,
                Title = "Test Movie",
                DateReleased = DateTime.Now,
                RatingId = 1,
                Showtimes = new List<Showtime>()
            },
            Tickets = new List<Ticket>()
        };

        _context.Showtime.Add(showtime);
        _context.SaveChanges();

        // Act
        var result = await _showtimeService.RemoveShowtime(2);

        // Assert
        Assert.True(result);
        Assert.Empty(await _context.Showtime.ToListAsync());
    }

    [Fact]
    public async Task RemoveShowtime_ShouldFail_WhenShowtimeNotFound()
    {
        // Act
        var result = await _showtimeService.RemoveShowtime(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task EditShowtime_ShouldUpdateShowtime_WhenValid()
    {
        // Arrange
        var showtime = new Showtime
        {
            Id = 3,
            StartTime = DateTime.Now.AddHours(2),
            Capacity = 100,
            MovieId = 1,
            Movie = new Movie
            {
                Id = 1,
                Title = "Test Movie",
                DateReleased = DateTime.Now,
                RatingId = 1,
                Showtimes = new List<Showtime>()
            },
            Tickets = new List<Ticket>()
        };

        _context.Showtime.Add(showtime);
        _context.SaveChanges();

        var updatedShowtime = new Showtime
        {
            StartTime = DateTime.Now.AddHours(5),
            Capacity = 150,
            MovieId = 1,
            Movie = new Movie
            {
                Id = 1,
                Title = "Updated Movie",
                DateReleased = DateTime.Now,
                RatingId = 2,
                Showtimes = new List<Showtime>()
            },
            Tickets = new List<Ticket>()
        };

        // Act
        var result = await _showtimeService.EditShowtime(3, updatedShowtime);

        // Assert
        Assert.True(result);
        var editedShowtime = await _context.Showtime.FirstAsync(s => s.Id == 3);
        Assert.Equal(150, editedShowtime.Capacity);
        Assert.Equal(updatedShowtime.StartTime, editedShowtime.StartTime);
    }

    [Fact]
    public async Task EditShowtime_ShouldFail_WhenShowtimeNotFound()
    {
        // Arrange
        var updatedShowtime = new Showtime
        {
            StartTime = DateTime.Now.AddHours(6),
            Capacity = 120,
            MovieId = 1,
            Movie = new Movie
            {
                Id = 1,
                Title = "Nonexistent Movie",
                DateReleased = DateTime.Now,
                RatingId = 2,
                Showtimes = new List<Showtime>()
            },
            Tickets = new List<Ticket>() // Fix: Initialize required property
        };

        // Act
        var result = await _showtimeService.EditShowtime(999, updatedShowtime);

        // Assert
        Assert.False(result);
    }


    [Fact]
    public async Task GetShowtimesByMovieId_ShouldReturnShowtimes_WhenMovieExists()
    {
        // Arrange
        var showtime = new Showtime
        {
            Id = 4,
            StartTime = DateTime.Now.AddHours(1),
            Capacity = 100,
            MovieId = 1,
            Movie = new Movie
            {
                Id = 1,
                Title = "Test Movie",
                DateReleased = DateTime.Now,
                RatingId = 1,
                Showtimes = new List<Showtime>()
            },
            Tickets = new List<Ticket>()
        };

        _context.Showtime.Add(showtime);
        _context.SaveChanges();

        // Act
        var showtimes = await _showtimeService.GetShowtimesByMovieId(1);

        // Assert
        Assert.NotNull(showtimes);
        Assert.Single(showtimes);
        Assert.Equal(4, showtimes.First().Id);
    }

    [Fact]
    public async Task GetShowtimesByMovieId_ShouldReturnEmpty_WhenMovieNotFound()
    {
        // Act
        var showtimes = await _showtimeService.GetShowtimesByMovieId(999);

        // Assert
        Assert.NotNull(showtimes);
        Assert.Empty(showtimes);
    }
}
