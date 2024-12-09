using CM.API.Models;
using CM.API.Services;
using CM.API.Data;
using CM.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using System;
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
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique DB
            .Options;

        _context = new AppDbContext(options);
        _mockMovieService = new Mock<IMovieService>();
        _showtimeService = new ShowtimeService(_context, _mockMovieService.Object);
    }

    private void ResetDatabase()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    private Movie SeedMovie()
    {
        var movie = new Movie
        {
            Id = Guid.NewGuid().GetHashCode(),
            Title = "Test Movie",
            DateReleased = DateTime.Now,
            RatingId = Guid.NewGuid().GetHashCode()
        };

        _context.Movies.Add(movie);
        _context.SaveChanges();
        return movie;
    }

    [Fact]
    public async Task AddShowtime_ShouldAddShowtime_WhenValid()
    {
        ResetDatabase();
        var movie = SeedMovie();

        var showtime = new Showtime
        {
            Id = Guid.NewGuid().GetHashCode(),
            StartTime = DateTime.Now.AddHours(3),
            Capacity = 100,
            MovieId = movie.Id
        };

        _mockMovieService.Setup(m => m.GetMovieById(movie.Id)).ReturnsAsync(movie);

        var result = await _showtimeService.AddShowtime(showtime);

        Assert.True(result);
        Assert.Contains(showtime, _context.Showtime);
    }

    [Fact]
    public async Task AddShowtime_ShouldFail_WhenShowtimeAlreadyExists()
    {
        ResetDatabase();
        var movie = SeedMovie();
        var showtimeId = Guid.NewGuid().GetHashCode();

        var showtime = new Showtime
        {
            Id = showtimeId,
            StartTime = DateTime.Now.AddHours(3),
            Capacity = 100,
            MovieId = movie.Id
        };

        _context.Showtime.Add(showtime);
        _context.SaveChanges();

        var result = await _showtimeService.AddShowtime(showtime);

        Assert.False(result);
    }

    [Fact]
    public async Task RemoveShowtime_ShouldRemoveShowtime_WhenValid()
    {
        ResetDatabase();
        var movie = SeedMovie();
        var showtimeId = Guid.NewGuid().GetHashCode();

        var showtime = new Showtime
        {
            Id = showtimeId,
            StartTime = DateTime.Now.AddHours(4),
            Capacity = 100,
            MovieId = movie.Id
        };

        _context.Showtime.Add(showtime);
        _context.SaveChanges();

        var result = await _showtimeService.RemoveShowtime(showtimeId);

        Assert.True(result);
        Assert.DoesNotContain(showtime, _context.Showtime);
    }

    [Fact]
    public async Task RemoveShowtime_ShouldFail_WhenShowtimeNotFound()
    {
        ResetDatabase();

        var result = await _showtimeService.RemoveShowtime(Guid.NewGuid().GetHashCode());

        Assert.False(result);
    }

    [Fact]
    public async Task EditShowtime_ShouldUpdateShowtime_WhenValid()
    {
        ResetDatabase();
        var movie = SeedMovie();
        var showtimeId = Guid.NewGuid().GetHashCode();

        var showtime = new Showtime
        {
            Id = showtimeId,
            StartTime = DateTime.Now.AddHours(2),
            Capacity = 100,
            MovieId = movie.Id
        };

        _context.Showtime.Add(showtime);
        _context.SaveChanges();

        var updatedShowtime = new Showtime
        {
            StartTime = DateTime.Now.AddHours(5),
            Capacity = 150,
            MovieId = movie.Id
        };

        var result = await _showtimeService.EditShowtime(showtimeId, updatedShowtime);

        Assert.True(result);

        var editedShowtime = await _context.Showtime.FirstAsync(s => s.Id == showtimeId);
        Assert.Equal(150, editedShowtime.Capacity);
        Assert.Equal(updatedShowtime.StartTime, editedShowtime.StartTime);
    }

    [Fact]
    public async Task EditShowtime_ShouldFail_WhenShowtimeNotFound()
    {
        ResetDatabase();

        var updatedShowtime = new Showtime
        {
            StartTime = DateTime.Now.AddHours(6),
            Capacity = 120
        };

        var result = await _showtimeService.EditShowtime(Guid.NewGuid().GetHashCode(), updatedShowtime);

        Assert.False(result);
    }

    [Fact]
    public async Task GetShowtimesByMovieId_ShouldReturnShowtimes_WhenMovieExists()
    {
        ResetDatabase();
        var movie = SeedMovie();
        var showtimeId = Guid.NewGuid().GetHashCode();

        var showtime = new Showtime
        {
            Id = showtimeId,
            StartTime = DateTime.Now.AddHours(1),
            Capacity = 100,
            MovieId = movie.Id
        };

        _context.Showtime.Add(showtime);
        _context.SaveChanges();

        var showtimes = await _showtimeService.GetShowtimesByMovieId(movie.Id);

        Assert.NotNull(showtimes);
        Assert.Single(showtimes);
        Assert.Equal(showtimeId, showtimes.First().Id);
    }

    [Fact]
    public async Task GetShowtimesByMovieId_ShouldReturnEmpty_WhenMovieNotFound()
    {
        ResetDatabase();

        var showtimes = await _showtimeService.GetShowtimesByMovieId(Guid.NewGuid().GetHashCode());

        Assert.NotNull(showtimes);
        Assert.Empty(showtimes);
    }
}
