using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IAccountService accountService, IConfiguration configuration, ILogger<AccountController> logger)
    {
        _accountService = accountService;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost("signup")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] UserCreateDto signupRequest)
    {
        var user = await _accountService.Register(signupRequest.Email, signupRequest.Username, signupRequest.Password, signupRequest.DateOfBirth);

        if (user == null)
            return BadRequest(new { message = "User registration failed" });

        var userDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            DateOfBirth = user.DateOfBirth
        };

        return Ok(userDto);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginRequest)
    {
        var user = await _accountService.Authenticate(loginRequest.Username, loginRequest.Password);

        if (user == null)
            return Unauthorized(new { message = "Invalid username or password" });

        var token = GenerateJwtToken(user);

        var userDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            DateOfBirth = user.DateOfBirth
        };

        return Ok(new { token, user = userDto });
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        // Extract the username from the JWT token claims
        var username = User.FindFirstValue(ClaimTypes.Name);
        _logger.LogInformation("Extracted username from token: {Username}", username);

        if (string.IsNullOrEmpty(username))
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var user = await _accountService.GetUserByUsername(username);
        if (user == null)
        {
            _logger.LogWarning("User not found for username: {Username}", username);
            return NotFound(new { message = "User not found" });
        }

        var userDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            DateOfBirth = user.DateOfBirth
        };

        return Ok(userDto);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Name, user.Username)
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}