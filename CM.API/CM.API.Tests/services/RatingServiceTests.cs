using CM.API.Data;
using CM.API.Models;
using CM.API.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CM.API.Tests.Services
{
    public class RatingServiceTests
    {
        private readonly AppDbContext _context;
        private readonly RatingService _service;

        public RatingServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);
            _service = new RatingService(_context);
        }

        [Fact]
        public async Task GetRatings_ReturnsAllRatings_WhenRatingsExist()
        {
            // Arrange
            var ratings = new List<Rating>
            {
                new Rating { Id = 1, Name = "Excellent" },
                new Rating { Id = 2, Name = "Good" }
            };
            await _context.Ratings.AddRangeAsync(ratings);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetRatings();

            // Assert
            Assert.Equal(ratings.Count, result.Count);
            Assert.Contains(result, r => r.Name == "Excellent");
            Assert.Contains(result, r => r.Name == "Good");
        }

        [Fact]
        public async Task GetRatings_ReturnsEmptyList_WhenNoRatingsExist()
        {
            // Arrange
            // Ensure the database is empty
            _context.Ratings.RemoveRange(_context.Ratings);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetRatings();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetRatings_DoesNotReturnNull()
        {
            // Act
            var result = await _service.GetRatings();

            // Assert
            Assert.NotNull(result);
        }
    }
}
