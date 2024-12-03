using Moq;
using Xunit;
using CM.API.Services;
using CM.API.Models;
using CM.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CM.API.Tests.services
{
    public class AccountServiceTests
    {
        private readonly Mock<ILogger<AccountService>> _mockLogger;
        private readonly AppDbContext _context;
        private readonly AccountService _service;

        public AccountServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new AppDbContext(options);
            _mockLogger = new Mock<ILogger<AccountService>>();
            _service = new AccountService(_context, _mockLogger.Object);
        }

        [Fact]
        public async Task Authenticate_ValidCredentials_ReturnsUserDto()
        {
            // Arrange
            var user = new User
            {
                Username = "testuser",
                Password = BCrypt.Net.BCrypt.HashPassword("password123"),
                Email = "test@example.com",
                DateOfBirth = DateTime.Now.AddYears(-25)
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.Authenticate("testuser", "password123");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("testuser", result.Username);
        }

        [Fact]
        public async Task Authenticate_InvalidCredentials_ReturnsNull()
        {
            // Act
            var result = await _service.Authenticate("invaliduser", "wrongpassword");

            // Assert
            Assert.Null(result);
        }
    }
}
