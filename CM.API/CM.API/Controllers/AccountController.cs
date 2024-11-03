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