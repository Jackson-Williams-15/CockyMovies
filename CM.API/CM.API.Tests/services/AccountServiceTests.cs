using CM.API.Data;
using CM.API.Models;
using CM.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CM.API.Tests
{
    public class AccountServiceTests
    {
        private readonly Mock<AppDbContext> _mockContext;
        private readonly Mock<ILogger<AccountService>> _mockLogger;
        private readonly AccountService _accountService;

        public AccountServiceTests()
        {
            _mockContext = new Mock<AppDbContext>();
            _mockLogger = new Mock<ILogger<AccountService>>();
            _accountService = new AccountService(_mockContext.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Authenticate_UserNotFound_ReturnsNull()
        {
            // Arrange
            var username = "testUser";
            var password = "testPassword";

            _mockContext.Setup(c => c.Users.FirstOrDefaultAsync(It.IsAny<Func<User, bool>>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _accountService.Authenticate(username, password);

            // Assert
            Assert.Null(result);
            _mockLogger.Verify(log => log.LogWarning(It.IsAny<string>(), username), Times.Once);
        }

        [Fact]
        public async Task Authenticate_InvalidPassword_ReturnsNull()
        {
            // Arrange
            var username = "testUser";
            var password = "wrongPassword";
            var user = new User { Username = username, Password = "hashedPassword" };

            _mockContext.Setup(c => c.Users.FirstOrDefaultAsync(It.IsAny<Func<User, bool>>()))
                .ReturnsAsync(user);

            // Act
            var result = await _accountService.Authenticate(username, password);

            // Assert
            Assert.Null(result);
            _mockLogger.Verify(log => log.LogWarning(It.IsAny<string>(), username), Times.Once);
        }

        [Fact]
        public async Task Authenticate_ValidUser_ReturnsUserDto()
        {
            // Arrange
            var username = "testUser";
            var password = "correctPassword";
            var user = new User
            {
                Id = 1,
                Username = username,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                Cart = new Cart { CartId = 1, UserId = 1 }
            };

            _mockContext.Setup(c => c.Users.Include(It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>())
                .FirstOrDefaultAsync(It.IsAny<Func<User, bool>>()))
                .ReturnsAsync(user);

            _mockContext.Setup(c => c.Carts.Include(It.IsAny<Func<IQueryable<Cart>, IIncludableQueryable<Cart, object>>>())
                .FirstOrDefaultAsync(It.IsAny<Func<Cart, bool>>()))
                .ReturnsAsync(user.Cart);

            // Act
            var result = await _accountService.Authenticate(username, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Username, result.Username);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task Register_NewUser_CreatesUserAndCart()
        {
            // Arrange
            var email = "test@example.com";
            var username = "newUser";
            var password = "password123";
            var dateOfBirth = new DateTime(1990, 1, 1);

            // Act
            var result = await _accountService.Register(email, username, password, dateOfBirth);

            // Assert
            _mockContext.Verify(c => c.Users.Add(It.IsAny<User>()), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(), Times.Exactly(2));
            Assert.NotNull(result);
            Assert.Equal(username, result.Username);
        }

        [Fact]
        public async Task UpdateUser_UserNotFound_ReturnsNull()
        {
            // Arrange
            var userId = "999";
            var updateDto = new UserUpdateDto
            {
                Email = "updated@example.com",
                Username = "updatedUser",
                DateOfBirth = new DateTime(1985, 5, 15)
            };

            _mockContext.Setup(c => c.Users.FirstOrDefaultAsync(It.IsAny<Func<User, bool>>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _accountService.UpdateUser(userId, updateDto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUser_ValidUser_UpdatesUser()
        {
            // Arrange
            var userId = "1";
            var updateDto = new UserUpdateDto
            {
                Email = "updated@example.com",
                Username = "updatedUser",
                DateOfBirth = new DateTime(1985, 5, 15)
            };
            var user = new User { Id = 1, Email = "old@example.com", Username = "oldUser", DateOfBirth = new DateTime(1980, 1, 1) };

            _mockContext.Setup(c => c.Users.FirstOrDefaultAsync(It.IsAny<Func<User, bool>>()))
                .ReturnsAsync(user);

            // Act
            var result = await _accountService.UpdateUser(userId, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateDto.Email, result.Email);
            Assert.Equal(updateDto.Username, result.Username);
        }

        [Fact]
        public async Task UpdatePassword_InvalidUserId_ReturnsError()
        {
            // Arrange
            var userId = "invalidId";
            var passwordUpdateDto = new UserPasswordDto
            {
                OldPassword = "oldPassword",
                NewPassword = "newPassword"
            };

            // Act
            var result = await _accountService.UpdatePassword(userId, passwordUpdateDto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Invalid User Id", result.Message);
        }

        [Fact]
        public async Task UpdatePassword_IncorrectOldPassword_ReturnsError()
        {
            // Arrange
            var userId = "1";
            var passwordUpdateDto = new UserPasswordDto
            {
                OldPassword = "wrongOldPassword",
                NewPassword = "newPassword"
            };
            var user = new User { Id = 1, Password = BCrypt.Net.BCrypt.HashPassword("correctOldPassword") };

            _mockContext.Setup(c => c.Users.FirstOrDefaultAsync(It.IsAny<Func<User, bool>>()))
                .ReturnsAsync(user);

            // Act
            var result = await _accountService.UpdatePassword(userId, passwordUpdateDto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Incorrect Old Password", result.Message);
        }

        [Fact]
        public async Task UpdatePassword_ValidPasswordChange_ReturnsSuccess()
        {
            // Arrange
            var userId = "1";
            var passwordUpdateDto = new UserPasswordDto
            {
                OldPassword = "correctOldPassword",
                NewPassword = "newPassword"
            };
            var user = new User { Id = 1, Password = BCrypt.Net.BCrypt.HashPassword("correctOldPassword") };

            _mockContext.Setup(c => c.Users.FirstOrDefaultAsync(It.IsAny<Func<User, bool>>()))
                .ReturnsAsync(user);

            // Act
            var result = await _accountService.UpdatePassword(userId, passwordUpdateDto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Password Updated Successfully", result.Message);
        }
    }
}
