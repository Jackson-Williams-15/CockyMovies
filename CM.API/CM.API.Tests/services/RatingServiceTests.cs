using CM.API.Models;
using CM.API.Services;
using CM.API.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RatingServiceTests
{
    private readonly RatingService _ratingService;
    private readonly AppDbContext _context;

    public RatingServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new AppDbContext(options);
        _ratingService = new RatingService(_context);

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        var ratings = new List<Rating>
        {
            new Rating { Id = 1, Name = "G" },
            new Rating { Id = 2, Name = "PG" },
            new Rating { Id = 3, Name = "PG-13" },
            new Rating { Id = 4, Name = "R" }
        };

        _context.Ratings.AddRange(ratings);
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetRatings_ShouldReturnRatings_WhenRatingsExist()
    {
        // Act
        var result = await _ratingService.GetRatings();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(4, result.Count);
        Assert.Contains(result, r => r.Name == "G");
        Assert.Contains(result, r => r.Name == "PG-13");
    }

    [Fact]
    public async Task GetRatings_ShouldReturnEmptyList_WhenNoRatingsExist()
    {
        // Arrange
        _context.Ratings.RemoveRange(_context.Ratings);
        await _context.SaveChangesAsync();

        // Act
        var result = await _ratingService.GetRatings();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
