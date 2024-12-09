// Import necessary namespaces for the test class
using CM.API.Data;                // Data access layer
using CM.API.Models;              // Models for database entities
using CM.API.Services;            // Business logic services
using Microsoft.EntityFrameworkCore; // Entity Framework Core for database management
using Microsoft.Extensions.Logging;  // Logging framework
using Moq;                         // Mocking framework
using System;                      // For DateTime and GUIDs
using System.Collections.Generic;  // For list collections
using System.Linq;                 // For LINQ queries
using System.Threading.Tasks;      // For async methods
using Xunit;                      // Testing framework

// Namespace for API tests
namespace CM.API.Tests
{
    // Test class for AccountService
    public class AccountServiceTests
    {
        private readonly Mock<ILogger<AccountService>> _mockLogger; // Mock logger for service
        private readonly AppDbContext _context;  // In-memory database context
        private readonly AccountService _accountService;  // Service under test

        // Constructor initializes in-memory database, logger, and service
        public AccountServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())  // Unique DB for test isolation
                .Options;

            _context = new AppDbContext(options); // Initialize database context
            _mockLogger = new Mock<ILogger<AccountService>>(); // Create mock logger
            _accountService = new AccountService(_context, _mockLogger.Object); // Create service instance
        }

        // Test successful user authentication
        [Fact]
        public async Task Authenticate_ShouldReturnUserDto_WhenUserExistsAndPasswordIsCorrect()
        {
            // Arrange: Create and save a test user
            var user = new User
            {
                Username = "testuser",
                Password = BCrypt.Net.BCrypt.HashPassword("password"),
                Email = "test@example.com",
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Role = "User"
            };

            _context.Users.Add(user);  // Add user to context
            await _context.SaveChangesAsync();  // Save changes

            // Act: Authenticate the user
            var result = await _accountService.Authenticate("testuser", "password");

            // Assert: Ensure correct user data is returned
            Assert.NotNull(result);  // Verify result is not null
            Assert.Equal("testuser", result.Username);  // Ensure correct username
            Assert.Equal("test@example.com", result.Email);  // Ensure correct email
        }

        // Test authentication with non-existent user
        [Fact]
        public async Task Authenticate_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Act: Attempt to authenticate nonexistent user
            var result = await _accountService.Authenticate("nonexistentuser", "password");

            // Assert: Ensure result is null
            Assert.Null(result);
        }

        // Test authentication with incorrect password
        [Fact]
        public async Task Authenticate_ShouldReturnNull_WhenPasswordIsIncorrect()
        {
            // Arrange: Create and save a test user
            var user = new User
            {
                Username = "testuser",
                Password = BCrypt.Net.BCrypt.HashPassword("password"),
                Email = "test@example.com",
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Role = "User"
            };

            _context.Users.Add(user);  // Add user to context
            await _context.SaveChangesAsync();  // Save changes

            // Act: Attempt authentication with wrong password
            var result = await _accountService.Authenticate("testuser", "wrongpassword");

            // Assert: Ensure result is null
            Assert.Null(result);
        }

        // Test successful user registration
        [Fact]
        public async Task Register_ShouldReturnUserDto_WhenUserIsRegistered()
        {
            // Arrange: Registration details
            var email = "newuser@example.com";
            var username = "newuser";
            var password = "password123";
            var dateOfBirth = DateTime.Parse("1995-01-01");

            // Act: Register new user
            var result = await _accountService.Register(email, username, password, dateOfBirth);

            // Assert: Verify user registration details
            Assert.NotNull(result);  // Ensure result is not null
            Assert.Equal(username, result.Username);  // Ensure correct username
            Assert.Equal(email, result.Email);  // Ensure correct email
            Assert.Equal(dateOfBirth, result.DateOfBirth);  // Ensure correct date of birth
        }

        // Test fetching user by username when user exists
        [Fact]
        public async Task GetUserByUsername_ShouldReturnUser_WhenUserExists()
        {
            // Arrange: Create and save a test user
            var user = new User
            {
                Username = "existinguser",
                Password = BCrypt.Net.BCrypt.HashPassword("password"),
                Email = "existinguser@example.com",
                DateOfBirth = DateTime.Parse("1990-01-01"),
                Role = "User"
            };

            _context.Users.Add(user);  // Add user to context
            await _context.SaveChangesAsync();  // Save changes

            // Act: Fetch user by username
            var result = await _accountService.GetUserByUsername("existinguser");

            // Assert: Verify user data
            Assert.NotNull(result);  // Ensure result is not null
            Assert.Equal("existinguser", result.Username);  // Ensure correct username
        }

        // Test fetching user by username when user doesn't exist
        [Fact]
        public async Task GetUserByUsername_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Act: Attempt to fetch nonexistent user
            var result = await _accountService.GetUserByUsername("nonexistentuser");

            // Assert: Ensure result is null
            Assert.Null(result);
        }

        // Test successful user update
        [Fact]
        public async Task UpdateUser_ShouldReturnUpdatedUserDto_WhenValidDataIsProvided()
        {
            // Arrange: Create and save a test user
            var user = new User
            {
                Username = "user1",
                Password = BCrypt.Net.BCrypt.HashPassword("password"),
                Email = "user1@example.com",
                DateOfBirth = DateTime.Parse("1990-01-01"),
                Role = "User"
            };

            _context.Users.Add(user);  // Add user to context
            await _context.SaveChangesAsync();  // Save changes

            // Update details
            var updateDto = new UserUpdateDto
            {
                Username = "updateduser",
                Email = "updateduser@example.com",
                DateOfBirth = DateTime.Parse("1992-01-01")
            };

            // Act: Update user
            var result = await _accountService.UpdateUser(user.Id.ToString(), updateDto);

            // Assert: Verify updated user details
            Assert.NotNull(result);  // Ensure result is not null
 