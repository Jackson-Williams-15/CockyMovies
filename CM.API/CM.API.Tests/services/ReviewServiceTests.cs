using CM.API.Models;
using CM.API.Services;
using CM.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

public class ReviewServiceTests
{
    private readonly ReviewService _reviewService;
    private readonly AppDbContext _context;
    private readonly ILogger<ReviewService> _mockLogger;

    public ReviewServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB per test
            .Options;

        _context = new AppDbContext(options);
        _mockLogger = new LoggerFactory().CreateLogger<ReviewService>();

        _reviewService = new ReviewService(
            _context,
            _mockLogger,
            new FakeContentModerationService()
        );
    }

    private void ResetDatabase()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    private void SeedDatabase()
    {
        ResetDatabase();

        // Ensure required properties are set
        _context.Users.Add(new User
        {
            Id = 1,
            Username = "TestUser",
            Email = "testuser@example.com",
            Password = "TestPassword"
        });

        _context.Reviews.AddRange(new List<Review>
        {
            new Review { Id = 1, Title = "Great Movie", Description = "Loved the story!", MovieId = 1, Rating = 5 },
            new Review { Id = 2, Title = "Not Good", Description = "Disappointing.", MovieId = 2, Rating = 2 }
        });

        _context.SaveChanges();
    }

    [Fact]
    public async Task AddReview_ShouldAddReview_WhenValid()
    {
        ResetDatabase();
        SeedDatabase();

        var review = new Review
        {
            Title = "Awesome Movie",
            Description = "Well directed!",
            MovieId = 1,
            Rating = 4
        };

        var result = await _reviewService.AddReview(review);

        Assert.True(result);
        Assert.Equal(3, _context.Reviews.Count());
    }

    [Fact]
    public async Task GetReviews_ShouldReturnReviews_WhenMovieHasReviews()
    {
        ResetDatabase();
        SeedDatabase();

        var reviews = await _reviewService.GetReviews(1);

        Assert.NotNull(reviews);
        Assert.Single(reviews);
        Assert.Equal("Great Movie", reviews.First().Title);
    }

    [Fact]
    public async Task EditReview_ShouldUpdateReview_WhenValid()
    {
        ResetDatabase();
        SeedDatabase();

        var updatedReview = new Review
        {
            Title = "Updated Title",
            Description = "Better Story",
            Rating = 4
        };

        var result = await _reviewService.EditReview(1, updatedReview);

        Assert.True(result);

        var editedReview = await _context.Reviews.AsNoTracking().FirstOrDefaultAsync(r => r.Id == 1);
        Assert.Equal("Updated Title", editedReview.Title);
        Assert.Equal("Better Story", editedReview.Description);
        Assert.Equal(4, editedReview.Rating);
    }

    [Fact]
    public async Task LikeReview_ShouldIncreaseLikes_WhenValid()
    {
        ResetDatabase();
        SeedDatabase();

        var result = await _reviewService.LikeReview(1);

        Assert.True(result);

        var likedReview = await _context.Reviews.AsNoTracking().FirstOrDefaultAsync(r => r.Id == 1);
        Assert.Equal(1, likedReview.Likes);
    }

    [Fact]
    public async Task AddReply_ShouldAddReply_WhenValid()
    {
        ResetDatabase();
        SeedDatabase();

        var reply = new Reply
        {
            ReviewId = 1,
            Body = "I agree with this review!",
        };

        var result = await _reviewService.AddReply(reply);

        Assert.True(result);
        Assert.Single(await _reviewService.GetReplies(1));
    }

    [Fact]
    public async Task RemoveReview_ShouldRemoveReview_WhenValid()
    {
        ResetDatabase();
        SeedDatabase();

        var result = await _reviewService.RemoveReview(1);

        Assert.True(result);

        var deletedReview = await _context.Reviews.FindAsync(1);
        Assert.Null(deletedReview);
    }

    [Fact]
    public async Task GetReviewById_ShouldReturnReview_WhenExists()
    {
        ResetDatabase();
        SeedDatabase();

        var review = await _reviewService.GetReviewById(1);

        Assert.NotNull(review);
        Assert.Equal("Great Movie", review.Title);
    }

    [Fact]
    public async Task GetReviewById_ShouldReturnEmpty_WhenNotFound()
    {
        ResetDatabase();
        SeedDatabase();

        var review = await _reviewService.GetReviewById(999); // Non-existent

        Assert.NotNull(review);
        Assert.Equal(default, review.Id); // Default if not found
    }
}

// Define a fake content moderation service for tests
public class FakeContentModerationService : ContentModerationService
{
    // Use "new" to avoid CS0506 error
    public new string CensorContent(string content) => content;
}
