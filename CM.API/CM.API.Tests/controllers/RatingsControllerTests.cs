using CM.API.Controllers;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class RatingsControllerTests
{
    private readonly Mock<IRatingService> _mockRatingService; // Mock for IRatingService
    private readonly RatingsController _controller; // The controller under test

    // Constructor to set up mock service and controller instance
    public RatingsControllerTests()
    {
        _mockRatingService = new Mock<IRatingService>(); // Initialize the mock for IRatingService
        _controller = new RatingsController(_mockRatingService.Object); // Initialize the controller with the mocked service
    }

    // Test for GetRatings method when ratings are successfully retrieved
    [Fact]
    public async Task GetRatings_ReturnsOk_WithRatingsList()
    {
        // Arrange: Create a list of ratings to return from the mock service
        var ratings = new List<Rating>
        {
            new Rating { Id = 1, Name = "G" },
            new Rating { Id = 2, Name = "PG" },
            new Rating { Id = 3, Name = "PG-13" }
        };

        _mockRatingService.Setup(s => s.GetRatings()).ReturnsAsync(ratings); // Mock GetRatings method

        // Act: Call the controller method
        var result = await _controller.GetRatings();

        // Assert: Verify the result is Ok and contains the expected list of ratings
        var okResult = Assert.IsType<OkObjectResult>(result); // Expect Ok result
        var returnedRatings = okResult.Value as List<Rating>; // Ensure the returned value is a List of Rating
        Assert.NotNull(returnedRatings); // Ensure returned ratings list is not null
        Assert.Equal(3, returnedRatings.Count); // Ensure the number of ratings is correct
    }

    // Test for GetRatings method when no ratings exist
    [Fact]
    public async Task GetRatings_ReturnsOk_WithEmptyList_WhenNoRatingsExist()
    {
        // Arrange: Mock GetRatings to return an empty list
        _mockRatingService.Setup(s => s.GetRatings()).ReturnsAsync(new List<Rating>()); // Mock the case where no ratings exist

        // Act: Call the controller method
        var result = await _controller.GetRatings();

        // Assert: Verify the result is Ok and contains an empty list
        var okResult = Assert.IsType<OkObjectResult>(result); // Expect Ok result
        var returnedRatings = okResult.Value as List<Rating>; // Ensure the returned value is a List of Rating
        Assert.NotNull(returnedRatings); // Ensure returned ratings list is not null
        Assert.Empty(returnedRatings); // Ensure the returned list is empty
    }
}
