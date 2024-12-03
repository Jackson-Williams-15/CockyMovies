using Moq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CM.API.Controllers;
using CM.API.Models;
using CM.API.Interfaces;
using CM.API.Data;
using Newtonsoft.Json.Linq;
using Xunit;

public class AccountControllerTests
{
    private readonly Mock<IAccountService> _mockAccountService;
    private readonly Mock<ICartService> _mockCartService;
    private readonly Mock<ILogger<AccountController>> _mockLogger;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly AccountController _accountController;
    private readonly AppDbContext _dbContext;

    public AccountControllerTests()
    {
        // Mock dependencies
        _mockLogger = new Mock<ILogger<AccountController>>();
        _mockAccountService = new Mock<IAccountService>();
        _mockCartService = new Mock<ICartService>();
        _mockConfiguration = new Mock<IConfiguration>();

        // Set up in-memory database for AppDbContext
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _dbContext = new AppDbContext(options);

        // Initialize the controller with mocks and the real DbContext
        _accountController = new AccountController(
            _mockAccountService.Object,
            _mockConfiguration.Object,
            _mockLogger.Object,
            _mockCartService.Object,
            _dbContext
        );
    }

[Fact]
public async Task SignUp_ShouldReturnOk_WhenUserIsRegistered()
{
    // Arrange
    var signupRequest = new UserCreateDto
    {
        Email = "test@example.com",
        Username = "testuser",
        Password = "Password123",
        DateOfBirth = new DateTime(1990, 1, 1)
    };

    var mockUser = new UserDto { Id = 1, Username = "testuser" };
    _mockAccountService.Setup(service => service.Register(
            signupRequest.Email,
            signupRequest.Username,
            signupRequest.Password,
            signupRequest.DateOfBirth))
        .ReturnsAsync(mockUser);

    var result = await controller.SignUp(signUpRequest);
    var okResult = Assert.IsType<OkObjectResult>(result); // Assert the result is OkObjectResult

    // Cast the value to an anonymous type
    var value = Assert.IsType<ExpandoObject>(okResult.Value);

    // You can now access the properties of the anonymous object
    dynamic data = value;
    Assert.Equal(userDto, data.user);
    Assert.Equal(cart?.CartId, data.cartId);
}




    // [Fact]
    // public async Task Login_ShouldReturnOk_WhenCredentialsAreValid()
    // {
    //     // Arrange
    //     var loginRequest = new UserLoginDto
    //     {
    //         Username = "testuser",
    //         Password = "Password123"
    //     };

    //     var mockUser = new UserDto { Id = 1, Username = "testuser" };
    //     _mockAccountService.Setup(service => service.Authenticate(
    //             loginRequest.Username,
    //             loginRequest.Password))
    //         .ReturnsAsync(mockUser);

    //     var mockCart = new Cart { CartId = 1 };
    //     _mockCartService.Setup(service => service.GetCartByUserId(It.IsAny<int>()))
    //         .ReturnsAsync(mockCart);

    //     _mockConfiguration.SetupGet(config => config["Jwt:Key"]).Returns("TestKey");
    //     _mockConfiguration.SetupGet(config => config["Jwt:Issuer"]).Returns("TestIssuer");

    //     // Act
    //     var result = await _accountController.Login(loginRequest);

    //     // Assert
    //     var okResult = Assert.IsType<OkObjectResult>(result);
    //     var response = Assert.IsType<dynamic>(okResult.Value);
    //     Assert.NotNull(response.token);
    //     Assert.Equal(1, response.cartId);
    // }

    // [Fact]
    // public async Task GetProfile_ShouldReturnUserProfile_WhenUserIsAuthenticated()
    // {
    //     // Arrange
    //     var userId = "1"; // Assume a valid user ID from JWT token
    //     var mockUser = new User
    //     {
    //         Id = 1,
    //         Email = "test@example.com",
    //         Username = "testuser",
    //         DateOfBirth = new DateTime(1990, 1, 1),
    //         PaymentDetails = new PaymentDetails { CardNumber = "1234", ExpiryDate = "12/25" }
    //     };

    //     _mockAccountService.Setup(service => service.GetUserById(userId))
    //         .ReturnsAsync(mockUser);

    //     var userDto = new UserDto
    //     {
    //         Id = 1,
    //         Email = "test@example.com",
    //         Username = "testuser",
    //         DateOfBirth = new DateTime(1990, 1, 1),
    //         PaymentDetails = new PaymentDetailsDto { CardNumber = "1234", ExpiryDate = "12/25" }
    //     };

    //     // Act
    //     var result = await _accountController.GetProfile();

    //     // Assert
    //     var okResult = Assert.IsType<OkObjectResult>(result);
    //     var response = Assert.IsType<UserDto>(okResult.Value);
    //     Assert.Equal(mockUser.Username, response.Username);
    //     Assert.Equal(mockUser.Email, response.Email);
    // }

    // [Fact]
    // public async Task UpdateUser_ShouldReturnOk_WhenUpdateIsSuccessful()
    // {
    //     // Arrange
    //     var userId = "1"; // Assume a valid user ID from JWT token
    //     var updateDto = new UserUpdateDto
    //     {
    //         Username = "updateduser",
    //         Email = "updated@example.com"
    //     };

    //     var updatedUser = new UserDto { Id = 1, Username = "updateduser", Email = "updated@example.com" };

    //     _mockAccountService.Setup(service => service.UpdateUser(userId, updateDto))
    //         .ReturnsAsync(updatedUser);

    //     // Act
    //     var result = await _accountController.UpdateUser(updateDto);

    //     // Assert
    //     var okResult = Assert.IsType<OkObjectResult>(result);
    //     var response = Assert.IsType<UserDto>(okResult.Value);
    //     Assert.Equal("updateduser", response.Username);
    // }

    // [Fact]
    // public async Task UpdatePassword_ShouldReturnOk_WhenPasswordUpdateIsSuccessful()
    // {
    //     // Arrange
    //     var passwordUpdateDto = new UserPasswordDto
    //     {
    //         OldPassword = "OldPassword123",
    //         NewPassword = "NewPassword123"
    //     };

    //     var userId = "1"; // Assume a valid user ID from JWT token
    //     _mockAccountService.Setup(service => service.UpdatePassword(userId, passwordUpdateDto))
    //         .ReturnsAsync((true, "Password updated successfully"));

    //     // Act
    //     var result = await _accountController.UpdatePassword(passwordUpdateDto);

    //     // Assert
    //     var okResult = Assert.IsType<OkObjectResult>(result);
    //     var response = Assert.IsType<dynamic>(okResult.Value);
    //     Assert.Equal("Password updated successfully", response.message);
    // }

    // [Fact]
    // public async Task SavePaymentDetails_ShouldReturnOk_WhenPaymentDetailsAreSaved()
    // {
    //     // Arrange
    //     var paymentDetailsDto = new PaymentDetailsDto
    //     {
    //         CardNumber = "1234",
    //         ExpiryDate = "12/25",
    //         CVV = "123",
    //         CardHolderName = "Test User",
    //         PaymentMethod = "CreditCard"
    //     };

    //     var userId = "1"; // Assume a valid user ID from JWT token
    //     var mockUser = new User { Id = 1, PaymentDetails = new PaymentDetails() };

    //     // Use the in-memory DbContext for this test
    //     var mockDbSet = new Mock<DbSet<User>>();
    //     mockDbSet.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(mockUser);

    //     var mockDbContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
    //     mockDbContext.Setup(db => db.Users).Returns(mockDbSet.Object);

    //     var controller = new AccountController(
    //         _mockAccountService.Object,
    //         _mockConfiguration.Object,
    //         _mockLogger.Object,
    //         _mockCartService.Object,
    //         mockDbContext.Object // Use the mockDbContext
    //     );

    //     // Act
    //     var result = await controller.SavePaymentDetails(paymentDetailsDto);

    //     // Assert
    //     var okResult = Assert.IsType<OkObjectResult>(result);
    //     var response = Assert.IsType<dynamic>(okResult.Value);
    //     Assert.Equal("Payment details saved successfully", response.message);
    // }
}
