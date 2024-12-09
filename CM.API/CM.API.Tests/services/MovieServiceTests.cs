// Import necessary namespaces for the test class
using CM.API.Models; // Models representing database entities
using CM.API.Services; // Business logic services
using CM.API.Repositories; // Repository for accessing data
using CM.API.Data; // Data access layer
using Microsoft.EntityFrameworkCore; // Entity Framework Core for in-memory database
using Xunit; // Testing framework
using System; // For DateTime and GUIDs
using System.Collections.Generic; // For lists and collections
using System.Threading.Tasks; // For async methods
using System.Linq; // For LINQ queries

// Test class for MovieService
public class MovieServiceTests
{
    private readonly MovieService _movieService; // Service being tested
    private readonly AppDbContext _context; // In-memory database context
    private readonly GenreRepository _genreRepository; // Genre repository for dependency injection

    // Constructor initializes in-memory database, repositories, and service
    public MovieServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique in-memory database per test
            .Options;

        _context = new AppDbContext(options); // Initialize database context
        _genreRepository = new GenreRepository(_context); // Initialize genre repository
        _movieService = new MovieService(_context, _genreRepository); // Create service instance
    }

    // Resets the database state before each test
    private void ResetDatabase()
    {
        _context.Database.EnsureDeleted(); // Delete database
        _context.Database.EnsureCreated(); // Recreate database
    }

    // Seeds the database with initial test data
    private void SeedDatabase()
    {
        var user = new User
        {
            Id = Guid.NewGuid().GetHashCode(),
            Username = "TestUser",
            Password = "TestPassword",
      