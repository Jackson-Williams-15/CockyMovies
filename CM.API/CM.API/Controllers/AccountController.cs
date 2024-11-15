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
    private readonly ICartService _cartService;

    public AccountController(IAccountService accountService, IConfiguration configuration, ILogger<AccountController> logger, ICartService cartService)
    {
        _accountService = accountService;
        _cartService = cartService;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost("signup")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] UserCreateDto signupRequest)
    {
        var userDto = await _accountService.Register(signupRequest.Email, signupRequest.Username, signupRequest.Password, signupRequest.DateOfBirth);

        if (userDto == null)
            return BadRequest(new { message = "User registration failed" });

        var cart = await _cartService.GetCartByUserId(userDto.Id);

        return Ok(new { user = userDto, cartId = cart?.CartId });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginRequest)
    {
        var userDto = await _accountService.Authenticate(loginRequest.Username, loginRequest.Password);

        if (userDto == null)
            return Unauthorized(new { message = "Invalid username or password" });

        var cart = await _cartService.GetCartByUserId(userDto.Id);
        var token = GenerateJwtToken(userDto);

        return Ok(new { token, user = userDto, cartId = cart?.CartId });
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        // Extract the UserId from the JWT token claims
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        _logger.LogInformation("Extracted username from token: {Username}", userId);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var user = await _accountService.GetUserById(userId);
        if (user == null)
        {
            _logger.LogWarning("User not found for username: {Username}", userId);
            return NotFound(new { message = "User not found" });
        }

        var userDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            DateOfBirth = user.DateOfBirth,
            Cart = user.Cart != null ? new CartDto
            {
                CartId = user.Cart.CartId,
                UserId = user.Cart.UserId,
                Tickets = user.Cart.Tickets.Select(t => new CartTicketDto
                {
                    Id = t.Id,
                    Price = t.Price,
                    Showtime = new ShowtimeDto
                    {
                        Id = t.Showtime.Id,
                        StartTime = t.Showtime.StartTime,
                        Movie = new MovieDto
                        {
                            Id = t.Showtime.Movie.Id,
                            Title = t.Showtime.Movie.Title,
                            Description = t.Showtime.Movie.Description,
                            DateReleased = t.Showtime.Movie.DateReleased,
                            Rating = t.Showtime.Movie.Rating != null ? t.Showtime.Movie.Rating.ToString() : string.Empty
                        }
                    }
                }).ToList()
            } : null
        };

        return Ok(userDto);
    }

    private string GenerateJwtToken(UserDto user)
    {
        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString(), user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
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

    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto updateDto)
    {
        _logger.LogInformation("Received update request for user.");

        if (updateDto == null)
        {
            _logger.LogWarning("Update request body is null.");
            return BadRequest(new { message = "Invalid request body" });
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            _logger.LogWarning("Invalid token: User ID not found in token.");
            return Unauthorized(new { message = "Invalid token" });
        }

        _logger.LogInformation("Updating user with ID: {UserId}", userId);

        var updatedUser = await _accountService.UpdateUser(userId, updateDto);
        if (updatedUser == null)
        {
            _logger.LogWarning("User update failed for user ID: {UserId}", userId);
            return BadRequest(new { message = "User update failed" });
        }

        _logger.LogInformation("User updated successfully for user ID: {UserId}", userId);
        return Ok(updatedUser);
    }
}