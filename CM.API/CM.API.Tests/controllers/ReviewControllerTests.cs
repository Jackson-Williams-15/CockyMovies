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
    private readonly Mock<IReviewService> _mockReviewService;
    private readonly ReviewsController _controller;

    public ReviewsControllerTests()
    {
        _mockReviewService = new Mock<IReviewService>();
        _controller = new ReviewsController(_mockReviewService.Object);
    }

    [Fact]
    public async Task AddReview_ReturnsOk_WhenReviewIsAddedSuccessfully()
    {
        // Arrange
        var reviewDto = new ReviewCreateDto
        {
            Title = "Great Movie",
            Rating = 5,
            Description = "I really enjoyed it!",
            MovieId = 1
        };

        _mockReviewService.Setup(s => s.AddReview(It.IsAny<Review>())).ReturnsAsync(true);

        // Act
        var result = await _controller.AddReview(reviewDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Review added successfully.", okResult.Value);
    }

    [Fact]
    public async Task AddReview_ReturnsBadRequest_WhenReviewIsNotAdded()
    {
        // Arrange
        var reviewDto = new ReviewCreateDto
        {
            Title = "Great Movie",
            Rating = 5,
            Description = "I really enjoyed it!",
            MovieId = 1
        };

        _mockReviewService.Setup(s => s.AddReview(It.IsAny<Review>())).ReturnsAsync(false);

        // Act
        var result = await _controller.AddReview(reviewDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Failed to add review.", badRequestResult.Value);
    }

    [Fact]
    public async Task GetReviews_ReturnsOk_WithReviewsList()
    {
        // Arrange
        int movieId = 1;
        var reviews = new List<Review>
        {
            new Review { Id = 1, Title = "Amazing!", Rating = 5, Description = "Loved it!", MovieId = movieId },
            new Review { Id = 2, Title = "Not Bad", Rating = 3, Description = "It was okay.", MovieId = movieId }
        };

        _mockReviewService.Setup(s => s.GetReviews(movieId)).ReturnsAsync(reviews);

        // Act
        var result = await _controller.GetReviews(movieId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedReviews = okResult.Value as List<ReviewDto>;

        Assert.NotNull(returnedReviews);
        Assert.Equal(2, returnedReviews.Count);
    }

    [Fact]
    public async Task GetReviews_ReturnsNotFound_WhenNoReviewsExist()
    {
        // Arrange
        int movieId = 1;
        _mockReviewService.Setup(s => s.GetReviews(movieId)).ReturnsAsync(new List<Review>());

        // Act
        var result = await _controller.GetReviews(movieId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
