using CM.API.Data;
using CM.API.Models;
using CM.API.Services;
using CM.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CM.API.Tests.Services
{
    public class ShowtimeServiceTests
    {
        private readonly AppDbContext _context;
        private readonly Mock<IMovieService> _mockMovieService;
        private readonly ShowtimeService _service;

        public ShowtimeServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);
            _mockMovieService = new Mock<IMovieService>();
            _service = new ShowtimeService(_context, _mockMovieService.Object);
        }

        [Fact]
        public async Task AddShowtime_AddsShowtimeAndTickets_WhenValidDataIsProvided()
        {
            // Arrange
            var movie = new Movie
            {
                Id = 1,
                Title = "Test Movie",
                Showtimes = new List<Showtime>()
            };
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            var showtime = new Showtime
            {
                Id = 1,
                StartTime = DateTime.Now.AddHours(1),
                MovieId = 1,
                Capacity = 5,
                Movie = movie
            };

            _mockMovieService
                .Setup(s => s.GetMovieById(1))
                .ReturnsAsync(movie);

            // Act
            var result = await _service.AddShowtime(showtime);

            // Assert
            Assert.True(result);
            var storedShowtime = await _context.Showtime.Include(s => s.Tickets).FirstOrDefaultAsync(s => s.Id == 1);
            Assert.NotNull(storedShowtime);
            Assert.Equal(showtime.Capacity, storedShowtime.Capacity);
            Assert.Equal(5, storedShowtime.Tickets.Count);
            Assert.Equal(5, storedShowtime.TicketsAvailable);
        }

        [Fact]
        public async Task AddShowtime_ReturnsFalse_WhenShowtimeIdAlreadyExists()
        {
            // Arrange
            var existingShowtime = new Showtime
            {
                Id = 1,
                StartTime = DateTime.Now.AddHours(1),
                MovieId = 1,
                Capacity = 10
            };
            await _context.Showtime.AddAsync(existingShowtime);
            await _context.SaveChangesAsync();

            var newShowtime = new Showtime
            {
                Id = 1, // Same ID as existing showtime
                StartTime = DateTime.Now.AddHours(2),
                MovieId = 1,
                Capacity = 5
            };

            // Act
            var result = await _service.AddShowtime(newShowtime);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetShowtimesByMovieId_ReturnsShowtimesForGivenMovieId()
        {
            // Arrange
            var movie = new Movie
            {
                Id = 1,
                Title = "Test Movie"
            };
            var showtimes = new List<Showtime>
            {
                new Showtime
                {
                    Id = 1,
                    StartTime = DateTime.Now.AddHours(1),
                    MovieId = 1,
                    Capacity = 10,
                    Movie = movie,
                    Tickets = new List<Ticket>
                    {
                        new Ticket { Id = 1, Price = 10.00m },
                        new Ticket { Id = 2, Price = 10.00m }
                    }
                },
                new Showtime
                {
                    Id = 2,
                    StartTime = DateTime.Now.AddHours(3),
                    MovieId = 1,
                    Capacity = 20,
                    Movie = movie,
                    Tickets = new List<Ticket>
                    {
                        new Ticket { Id = 3, Price = 15.00m }
                    }
                }
            };
            await _context.Movies.AddAsync(movie);
            await _context.Showtime.AddRangeAsync(showtimes);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetShowtimesByMovieId(1);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, s => s.Id == 1 && s.Tickets.Count == 2);
            Assert.Contains(result, s => s.Id == 2 && s.Tickets.Count == 1);
        }

        [Fact]
        public async Task GetShowtimesByMovieId_ReturnsEmptyList_WhenNoShowtimesExist()
        {
            // Arrange
            var movie = new Movie
            {
                Id = 1,
                Title = "Test Movie"
            };
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetShowtimesByMovieId(1);

            // Assert
            Assert.Empty(result);
        }
    }
}
