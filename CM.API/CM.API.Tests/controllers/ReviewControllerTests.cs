using CM.API.Controllers;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class ReviewsControllerTests
{
    private readonly Mock<IReviewService> _mockReviewService; // Mock for IReviewService
    private readonly ReviewsController _controller; // The controller under test

    // Constructor to set up mock service and controller instance
    public ReviewsControllerTests()
    {
        _mockReviewService = new Mock<IReviewService>(); // Initialize the mock for IReviewService
        _controller = new ReviewsController(_mockReviewService.Object); // Initialize the controller with the mocked service
    }

    // Test for AddReview method when the review is added successfully
    [Fact]
    public async Task AddReview_ReturnsOk_WhenReviewIsAddedSuccessfully()
    {
        // Arrange: Create a ReviewCreateDto to add
        var reviewDto = new ReviewCreateDto
        {
            Title = "Great Movie",
            Rating = 5,
            Description = "I really enjoyed it!",
            MovieId = 1
        };

        // Mock AddReview to return true (indicating successful addition)
        _mockReviewService.Setup(s => s.AddReview(It.IsAny<Review>())).ReturnsAsync(true);

        // Act: Call the controller method
        var result = await _controller.AddReview(reviewDto);

        // Assert: Verify the result is Ok and contains the success message
        var okResult = Assert.IsType<OkObjectResult>(result); // Expect Ok result
        Assert.Equal("Review added successfully.", okResult.Value); // Ensure the message is correct
    }

    // Test for AddReview method when the review is not added
    [Fact]
    public async Task AddReview_ReturnsBadRequest_WhenReviewIsNotAdded()
    {
        // Arrange: Create a ReviewCreateDto to add
        var reviewDto = new ReviewCreateDto
        {
            Title = "Great Movie",
            Rating = 5,
            Description = "I really enjoyed it!",
            MovieId = 1
        };

        // Mock AddReview to return false (indicating failure to add the review)
        _mockReviewService.Setup(s => s.AddReview(It.IsAny<Review>())).ReturnsAsync(false);

        // Act: Call the controller method
        var result = await _controller.AddReview(reviewDto);

        // Assert: Verify the result is BadRequest and contains the error message
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result); // Expect BadRequest result
        Assert.Equal("Failed to add review.", badRequestResult.Value); // Ensure the error message is correct
    }

    // Test for GetReviews method when reviews are found for a movie
    [Fact]
    public async Task GetReviews_ReturnsOk_WithReviewsList()
    {
        // Arrange: Create a list of reviews to return for a given movie
        int movieId = 1;
        var reviews = new List<Review>
        {
            new Review { Id = 1, Title = "Amazing!", Rating = 5, Description = "Loved it!", MovieId = movieId },
            new Review { Id = 2, Title = "Not Bad", Rating = 3, Description = "It was okay.", MovieId = movieId }
        };

        // Mock GetReviews to return the list of reviews for the specified movie
        _mockReviewService.Setup(s => s.GetReviews(movieId)).ReturnsAsync(reviews);

        // Act: Call the controller method
        var result = await _controller.GetReviews(movieId);

        // Assert: Verify the result is Ok and contains the expected list of reviews
        var okResult = Assert.IsType<OkObjectResult>(result); // Expect Ok result
        var returnedReviews = okResult.Value as List<ReviewDto>; // Ensure the returned value is a List of ReviewDto

        Assert.NotNull(returnedReviews); // Ensure the list of reviews is not null
        Assert.Equal(2, returnedReviews.Count); // Ensure the correct number of reviews is returned
    }

    // Test for GetReviews method when no reviews exist for a movie
    [Fact]
    public async Task GetReviews_ReturnsNotFound_WhenNoReviewsExist()
    {
        // Arrange: Mock GetReviews to return an empty list for the specified movie
        int movieId = 1;
        _mockReviewService.Setup(s => s.GetReviews(movieId)).ReturnsAsync(new List<Review>()); // No reviews exist

        // Act: Call the controller method
        var result = await _controller.GetReviews(movieId);

        // Assert: Verify the result is NotFound since no reviews exist for the movie
        Assert.IsType<NotFoundObjectResult>(result); // Expect NotFound result
    }
}
