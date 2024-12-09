using CM.API.Models;
using CM.API.Services;
using CM.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ReviewServiceTests
{
    private readonly ReviewService _reviewService;
    private readonly AppDbContext _context;
    private readonly Mock<ILogger<ReviewService>> _mockLogger;
    private readonly Mock<ContentModerationService> _mockContentModerationService;

    public ReviewServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new AppDbContext(options);
        _mockLogger = new Mock<ILogger<ReviewService>>();
        _mockContentModerationService = new Mock<ContentModerationService>();

        _reviewService = new ReviewService(
            _context,
            _mockLogger.Object,
            _mockContentModerationService.Object
        );

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        var reviews = new List<Review>
        {
            new Review { Id = 1, Title = "Great Movie", Description = "Loved the story!", MovieId = 1, Rating = 5 },
            new Review { Id = 2, Title = "Not Good", Description = "Disappointing.", MovieId = 2, Rating = 2 }
        };

        _context.Reviews.AddRange(reviews);
        _context.SaveChanges();
    }

    [Fact]
    public async Task AddReview_ShouldAddReview_WhenValid()
    {
        // Arrange
        var review = new Review
        {
            Id = 3,
            Title = "Awesome Movie",
            Description = "Well directed!",
            MovieId = 1,
            Rating = 4
        };

        _mockContentModerationService.Setup(m => m.CensorContent(It.IsAny<string>()))
            .Returns<string>(content => content);

        // Act
        var result = await _reviewService.AddReview(review);

        // Assert
        Assert.True(result);
        Assert.Equal(3, _context.Reviews.Count());
    }

    [Fact]
    public async Task AddReview_ShouldFail_WhenExceptionOccurs()
    {
        // Arrange
        var review = new Review
        {
            Title = "Broken Movie",
            Description = "Exception Triggered",
            MovieId = 999, // Non-existent movie ID
            Rating = 1
        };

        _mockContentModerationService.Setup(m => m.CensorContent(It.IsAny<string>()))
            .Throws(new System.Exception("Content moderation failed."));

        // Act & Assert
        await Assert.ThrowsAsync<System.Exception>(() => _reviewService.AddReview(review));
    }

    [Fact]
    public async Task GetReviews_ShouldReturnReviews_WhenMovieHasReviews()
    {
        // Act
        var reviews = await _reviewService.GetReviews(1);

        // Assert
        Assert.NotNull(reviews);
        Assert.Single(reviews);
        Assert.Equal("Great Movie", reviews.First().Title);
    }

    [Fact]
    public async Task GetReviews_ShouldReturnEmptyList_WhenMovieHasNoReviews()
    {
        // Act
        var reviews = await _reviewService.GetReviews(999);

        // Assert
        Assert.NotNull(reviews);
        Assert.Empty(reviews);
    }

    [Fact]
    public async Task EditReview_ShouldUpdateReview_WhenValid()
    {
        // Arrange
        var updatedReview = new Review
        {
            Title = "Updated Title",
            Description = "Better Story",
            Rating = 4
        };

        // Act
        var result = await _reviewService.EditReview(1, updatedReview);

        // Assert
        Assert.True(result);
        var editedReview = await _context.Reviews.FindAsync(1);
        Assert.Equal("Updated Title", editedReview.Title);
        Assert.Equal("Better Story", editedReview.Description);
        Assert.Equal(4, editedReview.Rating);
    }

    [Fact]
    public async Task EditReview_ShouldFail_WhenReviewNotFound()
    {
        // Arrange
        var updatedReview = new Review
        {
            Title = "Nonexistent Title",
            Description = "No Story",
            Rating = 1
        };

        // Act
        var result = await _reviewService.EditReview(999, updatedReview);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task LikeReview_ShouldIncrementLikes_WhenValid()
    {
        // Act
        var result = await _reviewService.LikeReview(1);

        // Assert
        Assert.True(result);
        var likedReview = await _context.Reviews.FindAsync(1);
        Assert.Equal(1, likedReview.Likes); // Default was 0
    }

    [Fact]
    public async Task LikeReview_ShouldFail_WhenReviewNotFound()
    {
        // Act
        var result = await _reviewService.LikeReview(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddReply_ShouldAddReply_WhenValid()
    {
        // Arrange
        var reply = new Reply
        {
            Id = 1,
            Body = "I agree!",
            ReviewId = 1
        };

        _mockContentModerationService.Setup(m => m.CensorContent(It.IsAny<string>()))
            .Returns<string>(content => content);

        // Act
        var result = await _reviewService.AddReply(reply);

        // Assert
        Assert.True(result);
        Assert.Single(_context.Reply);
    }

    [Fact]
    public async Task GetReplies_ShouldReturnReplies_WhenRepliesExist()
    {
        // Arrange
        var reply = new Reply
        {
            Id = 1,
            Body = "Totally agree!",
            ReviewId = 1
        };

        _context.Reply.Add(reply);
        await _context.SaveChangesAsync();

        // Act
        var replies = await _reviewService.GetReplies(1);

        // Assert
        Assert.NotNull(replies);
        Assert.Single(replies);
        Assert.Equal("Totally agree!", replies.First().Body);
    }

    [Fact]
    public async Task RemoveReview_ShouldRemoveReview_WhenValid()
    {
        // Act
        var result = await _reviewService.RemoveReview(1);

        // Assert
        Assert.True(result);
        Assert.Null(await _context.Reviews.FindAsync(1));
    }

    [Fact]
    public async Task RemoveReview_ShouldFail_WhenReviewNotFound()
    {
        // Act
        var result = await _reviewService.RemoveReview(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetReviewById_ShouldReturnReview_WhenExists()
    {
        // Act
        var review = await _reviewService.GetReviewById(1);

        // Assert
        Assert.NotNull(review);
        Assert.Equal("Great Movie", review.Title);
    }

    [Fact]
    public async Task GetReviewById_ShouldReturnEmpty_WhenNotFound()
    {
        // Act
        var review = await _reviewService.GetReviewById(999);

        // Assert
        Assert.NotNull(review);
        Assert.Equal(0, review.Id); // Review should be empty
    }
}
