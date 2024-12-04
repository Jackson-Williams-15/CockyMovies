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
    private readonly Mock<IRatingService> _mockRatingService;
    private readonly RatingsController _controller;

    public RatingsControllerTests()
    {
        _mockRatingService = new Mock<IRatingService>();
        _controller = new RatingsController(_mockRatingService.Object);
    }

    [Fact]
    public async Task GetRatings_ReturnsOk_WithRatingsList()
    {
        // Arrange
        var ratings = new List<Rating>
        {
            new Rating { Id = 1, Name = "G" },
            new Rating { Id = 2, Name = "PG" },
            new Rating { Id = 3, Name = "PG-13" }
        };

        _mockRatingService.Setup(s => s.GetRatings()).ReturnsAsync(ratings);

        // Act
        var result = await _controller.GetRatings();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedRatings = okResult.Value as List<Rating>;

        Assert.NotNull(returnedRatings);
        Assert.Equal(3, returnedRatings.Count);
    }

    [Fact]
    public async Task GetRatings_ReturnsOk_WithEmptyList_WhenNoRatingsExist()
    {
        // Arrange
        _mockRatingService.Setup(s => s.GetRatings()).ReturnsAsync(new List<Rating>());

        // Act
        var result = await _controller.GetRatings();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedRatings = okResult.Value as List<Rating>;

        Assert.NotNull(returnedRatings);
        Assert.Empty(returnedRatings);
    }
}
