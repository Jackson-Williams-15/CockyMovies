// Import necessary namespaces for the test class
using CM.API.Models; // Models representing database entities
using CM.API.Services; // Services handling business logic
using CM.API.Data; // Data access layer
using Microsoft.EntityFrameworkCore; // Entity Framework Core for in-memory database
using Microsoft.Extensions.Logging; // Logging service
using Xunit; // Testing framework
using System.Collections.Generic; // For collections
using System.Linq; // For LINQ queries
using System.Threading.Tasks; // For async methods
using System; // For GUIDs and DateTime

// Test class for ReviewService
public class ReviewServiceTests
{
    private readonly ReviewService _reviewService; // Service under test
    private readonly AppDbContext _context; // In-memory database context
    private readonly ILogger<ReviewService> _mockLogger; // Mock logger
    private readonly ContentModerationService _mockContentModerationService; // Mock content moderation service

    // Constructor initializes in-memory database, logger, and service
    public ReviewServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database per test
            .Options;

        _context = new AppDbContext(options); // Initialize database context
        _mockLogger = new LoggerFactory().CreateLogger<ReviewService>(); // Create mock logger
        _mockContentModerationService = new ContentModerationService(); // Create mock content moderation service

        _reviewService = new ReviewService(
            _context,
            _mockLogger,
            _mockContentModerationService // Inject services
        );
    }

    // Resets database state before each test
    private void ResetDatabase()
    {
        _context.Database.EnsureDeleted(); // Delete database
        _context.Database.EnsureCreated(); // Recreate database
    }

    // Seeds database with initial data
    private void SeedDatabase()
    {
        ResetDatabase(); // Reset database first

        // Add sample user
        _context.Users.Add(new User
        {
            Username = "TestUser",
            Email = "testuser@example.com",
            Password = "TestPassword"
        });

        // Add sample reviews
        _context.Reviews.AddRange(new List<Review>
        {
            new Review { Title = "Great Movie", Description = "Loved the story!", MovieId = 1, Rating = 5 },
            new Review { Title = "Not Good", Description = "Disappointing.", MovieId = 2, Rating = 2 }
        });

        _context.SaveChanges(); // Save changes to database
    }

    // Test adding a valid review
    [Fact]
    public async Task AddReview_ShouldAddReview_WhenValid()
    {
        ResetDatabase(); // Reset DB
        SeedDatabase(); // Seed data

        // Create new review
        var review = new Review
        {
            Title = "Awesome Movie",
            Description = "Well directed!",
            MovieId = 1,
            Rating = 4
        };

        var result = await _reviewService.AddReview(review); // Call service

        Assert.True(result); // Verify success
        Assert.Equal(3, _context.Reviews.Count()); // Verify review count
    }

    // Test retrieving reviews for a movie with existing reviews
    [Fact]
    public async Task GetReviews_ShouldReturnReviews_WhenMovieHasReviews()
    {
        ResetDatabase(); // Reset DB
        SeedDatabase(); // Seed data

        var reviews = await _reviewService.GetReviews(1); // Call service

        Assert.NotNull(reviews); // Verify reviews exist
        Assert.Single(reviews); // Verify one review exists
        Assert.Equal("Great Movie", reviews.First().Title); // Verify correct review title
    }

    // Test editing a valid review
    [Fact]
    public async Task EditReview_ShouldUpdateReview_WhenValid()
    {
        ResetDatabase(); // Reset DB
        SeedDatabase(); // Seed data

        // Create updated review
        var updatedReview = new Review
        {
            Title = "Updated Title",
            Description = "Better Story",
            Rating = 4
        };

        var result = await _reviewService.EditReview(1, updatedReview); // Call service

        Assert.True(result); // Verify success

        // Verify updated review data
        var editedReview = await _context.Reviews.AsNoTracking().FirstOrDefaultAsync(r => r.Id == 1);
        Assert.Equal("Updated Title", editedReview.Title);
        Assert.Equal("Better Story", editedReview.Description);
        Assert.Equal(4, editedReview.Rating);
    }

    // Test liking a valid review
    [Fact]
    public async Task LikeReview_ShouldIncreaseLikes_WhenValid()
    {
        ResetDatabase(); // Reset DB
        SeedDatabase(); // Seed data

        var result = await _reviewService.LikeReview(1); // Call service

        Assert.True(result); // Verify success

        // Verify updated like count
        var likedReview = await _context.Reviews.AsNoTracking().FirstOrDefaultAsync(r => r.Id == 1);
        Assert.Equal(1, likedReview.Likes);
    }

    // Test adding a valid reply to a review
    [Fact]
    public async Task AddReply_ShouldAddReply_WhenValid()
    {
        ResetDatabase(); // Reset DB
        SeedDatabase(); // Seed data

        // Create new reply
        var reply = new Reply
        {
            ReviewId = 1,
            Body = "I agree with this review!",
            Author = "TestUser" // Ensure Author is set
        };

        var result = await _reviewService.AddReply(reply); // Call service

        Assert.True(result); // Verify success
        Assert.Single(await _reviewService.GetReplies(1)); // Verify reply count
    }

    // Test removing a valid review
    [Fact]
    public async Task RemoveReview_ShouldRemoveReview_WhenValid()
    {
        ResetDatabase(); // Reset DB
        SeedDatabase(); // Seed data

        var result = await _reviewService.RemoveReview(1); // Call service

        Assert.True(result); // Verify success

        // Verify review is deleted
        var deletedReview = await _context.Reviews.FindAsync(1);
        Assert.Null(deletedReview);
    }

    // Test fetching a review by ID when it exists
    [Fact]
    public async Task GetReviewById_ShouldReturnReview_WhenExists()
    {
        ResetDatabase(); // Reset DB
        SeedDatabase(); // Seed data

        var review = await _reviewService.GetReviewById(1); // Call service

        Assert.NotNull(review); // Verify review exists
        Assert.Equal("Great Movie", review.Title); // Verify correct title
    }
}
