using CM.API.Data;
using CM.API.Models;
using CM.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CM.API.Tests
{
    public class AccountServiceTests
    {
        private readonly Mock<ILogger<AccountService>> _mockLogger;
        private readonly AppDbContext _context;
        private readonly AccountService _accountService;

        public AccountServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _mockLogger = new Mock<ILogger<AccountService>>();
            _accountService = new AccountService(_context, _mockLogger.Object);
        }

        [Fact]
        public async Task Authenticate_ShouldReturnUserDto_WhenUserExistsAndPasswordIsCorrect()
        {
            // Arrange
            var user = new User
            {
                Username = "testuser",
                Password = BCrypt.Net.BCrypt.HashPassword("password"),
                Email = "test@example.com",
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _accountService.Authenticate("testuser", "password");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("testuser", result.Username);
            Assert.Equal("test@example.com", result.Email);
        }

        [Fact]
        public async Task Authenticate_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Act
            var result = await _accountService.Authenticate("nonexistentuser", "password");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Authenticate_ShouldReturnNull_WhenPasswordIsIncorrect()
        {
            // Arrange
            var user = new User
            {
                Username = "testuser",
                Password = BCrypt.Net.BCrypt.HashPassword("password"),
                Email = "test@example.com",
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _accountService.Authenticate("testuser", "wrongpassword");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Register_ShouldReturnUserDto_WhenUserIsRegistered()
        {
            // Arrange
            var email = "newuser@example.com";
            var username = "newuser";
            var password = "password123";
            var dateOfBirth = DateTime.Parse("1995-01-01");

            // Act
            var result = await _accountService.Register(email, username, password, dateOfBirth);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(username, result.Username);
            Assert.Equal(email, result.Email);
            Assert.Equal(dateOfBirth, result.DateOfBirth);
        }

        [Fact]
        public async Task GetUserByUsername_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var user = new User
            {
                Username = "existinguser",
                Password = BCrypt.Net.BCrypt.HashPassword("password"),
                Email = "existinguser@example.com",
                DateOfBirth = DateTime.Parse("1990-01-01"),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _accountService.GetUserByUsername("existinguser");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("existinguser", result.Username);
        }

        [Fact]
        public async Task GetUserByUsername_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Act
            var result = await _accountService.GetUserByUsername("nonexistentuser");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnUpdatedUserDto_WhenValidDataIsProvided()
        {
            // Arrange
            var user = new User
            {
                Username = "user1",
                Password = BCrypt.Net.BCrypt.HashPassword("password"),
                Email = "user1@example.com",
                DateOfBirth = DateTime.Parse("1990-01-01"),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var updateDto = new UserUpdateDto
            {
                Username = "updateduser",
                Email = "updateduser@example.com",
                DateOfBirth = DateTime.Parse("1992-01-01")
            };

            // Act
            var result = await _accountService.UpdateUser(user.Id.ToString(), updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("updateduser", result.Username);
            Assert.Equal("updateduser@example.com", result.Email);
            Assert.Equal(DateTime.Parse("1992-01-01"), result.DateOfBirth);
        }

        [Fact]
        public async Task UpdatePassword_ShouldReturnSuccess_WhenOldPasswordIsCorrect()
        {
            // Arrange
            var user = new User
            {
                Username = "user1",
                Password = BCrypt.Net.BCrypt.HashPassword("oldpassword"),
                Email = "user1@example.com",
                DateOfBirth = DateTime.Parse("1990-01-01"),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var passwordUpdateDto = new UserPasswordDto
            {
                OldPassword = "oldpassword",
                NewPassword = "newpassword"
            };

            // Act
            var result = await _accountService.UpdatePassword(user.Id.ToString(), passwordUpdateDto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Password Updated Successfully", result.Message);
        }

        [Fact]
        public async Task UpdatePassword_ShouldReturnFailure_WhenOldPasswordIsIncorrect()
        {
            // Arrange
            var user = new User
            {
                Username = "user1",
                Password = BCrypt.Net.BCrypt.HashPassword("oldpassword"),
                Email = "user1@example.com",
                DateOfBirth = DateTime.Parse("1990-01-01"),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var passwordUpdateDto = new UserPasswordDto
            {
                OldPassword = "wrongpassword",
                NewPassword = "newpassword"
            };

            // Act
            var result = await _accountService.UpdatePassword(user.Id.ToString(), passwordUpdateDto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Incorrect Old Password", result.Message);
        }
    }
}
