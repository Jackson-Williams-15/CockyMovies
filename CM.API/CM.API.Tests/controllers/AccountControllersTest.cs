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
    private readonly Mock<IAccountService> _mockAccountService;
    private readonly Mock<ICartService> _mockCartService;
    private readonly Mock<ILogger<AccountController>> _mockLogger;
    private readonly Mock<IConfiguration> _mockConfiguration;

    private readonly AccountController _controller;

    public AccountControllerTests()
    {
        _mockAccountService = new Mock<IAccountService>();
        _mockCartService = new Mock<ICartService>();
        _mockLogger = new Mock<ILogger<AccountController>>();
        _mockConfiguration = new Mock<IConfiguration>();

        _controller = new AccountController(
            _mockAccountService.Object,
            _mockConfiguration.Object,
            _mockLogger.Object,
            _mockCartService.Object,
            null // Assuming AppDbContext is not relevant for these tests
        );

        // Mock JWT configuration
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

    _mockAccountService.Setup(s => s.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
        .ReturnsAsync(userDto);

    _mockCartService.Setup(s => s.GetCartByUserId(It.IsAny<int>()))
        .ReturnsAsync(new Cart { CartId = 1, UserId = 1 });

    // Act
    var result = await _controller.SignUp(signupRequest);

    // Assert
    var okResult = Assert.IsType<OkObjectResult>(result);
    var response = okResult.Value as SignUpResponseDto;

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

        _mockAccountService.Setup(s => s.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((UserDto?)null);

        // Act
        var result = await _controller.Login(loginRequest);

        // Assert
        Assert.IsType<UnauthorizedObjectResult>(result);
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
        Email = "test@example.com"
    };

    _mockAccountService.Setup(s => s.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
        .ReturnsAsync(userDto);

    _mockCartService.Setup(s => s.GetCartByUserId(It.IsAny<int>()))
        .ReturnsAsync(new Cart
        {
            CartId = 1,
            UserId = 1
        });

    _mockConfiguration.Setup(c => c["Jwt:Key"]).Returns("dummykeydummykeydummykeydummykey");
    _mockConfiguration.Setup(c => c["Jwt:Issuer"]).Returns("testissuer");

    // Act
    var result = await _controller.Login(loginRequest);

    // Assert
    var okResult = Assert.IsType<OkObjectResult>(result);
    var response = okResult.Value as LoginResponseDto;

    Assert.NotNull(response.Token);
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
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = okResult.Value as UserDto;

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

    _mockAccountService.Setup(s => s.GetUserById(It.IsAny<string>()))
        .ReturnsAsync((User?)null);

    // Act
    var result = await _controller.GetProfile();

    // Assert
    Assert.IsType<NotFoundObjectResult>(result);
}

}
