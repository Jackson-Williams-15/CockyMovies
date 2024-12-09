// Import necessary namespaces for the test class
using CM.API.Models; // Models representing database entities
using CM.API.Services; // Business logic services
using CM.API.Data; // Data access layer
using Microsoft.EntityFrameworkCore; // Entity Framework Core for in-memory database
using Xunit; // Testing framework
using System.Collections.Generic; // For collections
using System.Threading.Tasks; // For async methods

// Test class for RatingService
public class RatingServiceTests
{
    private readonly RatingService _ratingService; // Service being tested
    private readonly AppDbContext _context; // In-memory database context

    // Constructor initializes in-memory database and service
    public RatingServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb") // Unique DB for test
            .Options;

        _context = new AppDbContext(options); // Create database context
        _ratingService = new RatingService(_context); // Create service instance

        SeedDatabase(); // Seed initial test data
    }

    // Seeds the database with sample rating data
    private void SeedDatabase()
    {
        var ratings = new List<Rating>
        {
            new Rating { Id = 1, Name = "G" },       // General audiences
            new Rating { Id = 2, Name = "PG" },     // Parental guidance suggested
            new Rating { Id = 3, Name = "PG-13" },  // Parents strongly cautioned
            new Rating { Id = 4, Name = "R" }       // Restricted
        };

        _context.Ratings.AddRange(ratings); // Add ratings to database
        _context.SaveChanges(); // Save changes
    }

    // Test to ensure retrieving ex