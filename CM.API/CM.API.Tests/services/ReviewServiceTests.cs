using CM.API.Data;
using CM.API.Models;
using CM.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CM.API.Tests.Services
{
    public class ReviewServiceTests
    {
        private readonly AppDbContext _context;
        private readonly Mock<ILogger<ReviewService>> _mockLogger;
        private readonly ReviewService _service;

        public ReviewServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);
            _mockLogger = new Mock<ILogger<ReviewService>>();
            _service = new ReviewService(_context, _mockLogger.Object);
        }

        [Fact]
        public async Task AddReview_AddsReviewToDatabase()
        {
            // Arrange
            var review = new Review
            {
                Title = "Great Movie",
                Rating = 5,
                Description = "An amazing experience!",
                MovieId = 1
            };

            // Act
            var result = await _service.AddReview(review);

            // Assert
            Assert.True(result);
            var storedReview = await _context.Reviews.FirstOrDefaultAsync(r => r.Title == "Great Movie");
            Assert.NotNull(storedReview);
            Assert.Equal(review.Rating, storedReview.Rating);
            Assert.Equal(review.Description, storedReview.Description);
        }

        [Fact]
        public async Task AddReview_ThrowsExceptionAndLogsError_OnFailure()
        {
            // Arrange
            var review = new Review
            {
                Title = null, // Invalid data to force an exception
                Rating = 5,
                Description = "Invalid review",
                MovieId = 1
            };

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(async () => await _service.AddReview(review));
            _mockLogger.Verify(
                logger => logger.LogError(
                    It.IsAny<Exception>(),
                    It.Is<string>(s => s.Contains("Error adding review"))
                ),
                Times.Once
            );
        }

        [Fact]
        public async Task GetReviews_ReturnsReviewsForMovieId()
        {
            // Arrange
            var reviews = new List<Review>
            {
                new Review { Title = "Great Movie", Rating = 5, Description = "Loved it!", MovieId = 1 },
                new Review { Title = "Good Movie", Rating = 4, Description = "Pretty good.", MovieId = 1 },
                new Review { Title = "Okay Movie", Rating = 3, Description = "It was alright.", MovieId = 2 }
            };
            await _context.Reviews.AddRangeAsync(reviews);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetReviews(1);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, r => Assert.Equal(1, r.MovieId));
        }

        [Fact]
        public async Task GetReviews_ReturnsEmptyList_WhenNoReviewsExistForMovieId()
        {
            // Arrange
            var reviews = new List<Review>
            {
                new Review { Title = "Okay Movie", Rating = 3, Description = "It was alright.", MovieId = 2 }
            };
            await _context.Reviews.AddRangeAsync(reviews);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetReviews(1);

            // Assert
            Assert.Empty(result);
        }
    }
}
