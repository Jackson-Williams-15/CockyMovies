using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CM.API.Controllers;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System.Collections.Generic;

public class AccountControllerTests
{
    private readonly Mock<IAccountService> _mockAccountService;  // Mock for IAccountService
    private readonly Mock<ICartService> _mockCartService;      // Mock for ICartService
    private readonly Mock<ILogger<AccountController>> _mockLogger;  // Mock for ILogger
    private readonly Mock<IConfiguration> _mockConfiguration;    // Mock for IConfiguration
    private readonly Mock<IEmailService> _mockEmailService;  // Mock for IEmailService

    private readonly AccountController _controller;

    // Constructor to set up mock objects and initialize the controller
    public AccountControllerTests()
    {
        _mockAccountService = new Mock<IAccountService>();
        _mockCartService = new Mock<ICartService>();
        _mockLogger = new Mock<ILogger<AccountController>>();
        _mockConfiguration = new Mock<IConfiguration>();
        _mockEmailService = new Mock<IEmailService>();  // Initialize the mock for IEmailService

        // Create the AccountController instance with mocks
        _controller = new AccountController(
            _mockAccountService.Object,
            _mockConfiguration.Object,
            _mockLogger.Object,
            _mockCartService.Object,
            null,  // Assuming AppDbContext is not relevant for these tests
            _mockEmailService.Object  // Pass the mock to the controller constructor
        );

        // Mock configuration settings for JWT (used in authentication)
        _mockConfiguration.Setup(c => c["Jwt:Key"]).Returns("dummykeydummykeydummykeydummykey");
        _mockConfiguration.Setup(c => c["Jwt:Issuer"]).Returns("testissuer");
    }

    [Fact]
    public async Task SignUp_ReturnsOk_WhenRegistrationIsSuccessful()
    {
        // Arrange
        var signupRequest = new UserCreateDto
        {
            Email = "test@example.com",
            Username = "testuser",
            Password = "password",
            DateOfBirth = DateTime.Now.AddYears(-20)
        };

        var userDto = new UserDto
        {
            Id = 1,
            Email = "test@example.com",
            Username = "testuser",
            Cart = new CartDto
            {
                CartId = 1,
                UserId = 1,
                Tickets = new List<CartTicketDto>()
            }
        };

        // Set up mock to return userDto when Register is called
        _mockAccountService.Setup(s => s.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
            .ReturnsAsync(userDto);

        // Set up mock for CartService
        _mockCartService.Setup(s => s.GetCartByUserId(It.IsAny<int>()))
            .ReturnsAsync(new Cart { CartId = 1, UserId = 1 });

        // Mock email service call
        _mockEmailService.Setup(e => e.SendEmail(It.IsAny<string>(), It.IsAny<EmailType>(), It.IsAny<User>(), It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.SignUp(signupRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);  // Check if the result is Ok
        var response = okResult.Value as SignUpResponseDto;

        // Validate response properties
        Assert.NotNull(response);
        Assert.Equal(userDto.Email, response.User.Email);
        Assert.Equal(userDto.Id, response.User.Id);
        Assert.Equal(userDto.Username, response.User.Username);
        Assert.Equal(1, response.CartId);
    }

    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenAuthenticationFails()
    {
        // Arrange
        var loginRequest = new UserLoginDto { Username = "testuser", Password = "wrongpassword" };

        // Mock authentication to return null when invalid credentials are provided
        _mockAccountService.Setup(s => s.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((UserDto?)null);

        // Act
        var result = await _controller.Login(loginRequest);

        // Assert
        Assert.IsType<UnauthorizedObjectResult>(result);  // Expect Unauthorized response
    }

    [Fact]
    public async Task Login_ReturnsOk_WhenAuthenticationIsSuccessful()
    {
        // Arrange
        var loginRequest = new UserLoginDto { Username = "testuser", Password = "password" };

        var userDto = new UserDto
        {
            Id = 1,
            Username = "testuser",
            Email = "test@example.com",
            Role = "User"
        };

        // Set up mock to return userDto for valid credentials
        _mockAccountService.Setup(s => s.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(userDto);

        // Set up mock for CartService
        _mockCartService.Setup(s => s.GetCartByUserId(It.IsAny<int>()))
            .ReturnsAsync(new Cart
            {
                CartId = 1,
                UserId = 1
            });

        // Act
        var result = await _controller.Login(loginRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);  // Expect Ok response
        var response = okResult.Value as LoginResponseDto;

        // Validate response properties
        Assert.NotNull(response.Token);  // Ensure token is present
        Assert.Equal("testuser", response.User.Username);
        Assert.Equal("test@example.com", response.User.Email);
        Assert.Equal(1, response.CartId);
    }

    [Fact]
    public async Task GetProfile_ReturnsOk_WhenUserIsFound()
    {
        // Arrange
        var userId = "1";
        var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] 
        {
            new Claim(ClaimTypes.NameIdentifier, userId)
        }));

        var user = new User
        {
            Id = 1,
            Email = "test@example.com",
            Username = "testuser",
            DateOfBirth = DateTime.Now.AddYears(-25)
        };

        _mockAccountService.Setup(s => s.GetUserById(It.IsAny<string>()))
            .ReturnsAsync(user);

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = claims }
        };

        // Act
        var result = await _controller.GetProfile();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);  // Expect Ok response
        var response = okResult.Value as UserDto;

        // Validate response properties
        Assert.NotNull(response);
        Assert.Equal(user.Id, response.Id);
        Assert.Equal(user.Email, response.Email);
        Assert.Equal(user.Username, response.Username);
    }

    [Fact]
    public async Task GetProfile_ReturnsNotFound_WhenUserIsNotFound()
    {
        // Arrange
        var userId = "1";
        var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] 
        {
            new Claim(ClaimTypes.NameIdentifier, userId)
        }));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = claims }
        };

        // Set up mock to return null when user is not found
        _mockAccountService.Setup(s => s.GetUserById(It.IsAny<string>()))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _controller.GetProfile();

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);  // Expect NotFound response
    }
}
