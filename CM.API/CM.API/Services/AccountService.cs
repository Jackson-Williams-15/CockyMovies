using CM.API.Data;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CM.API.Services;

public class AccountService : IAccountService
{
    private readonly AppDbContext _context;
    private readonly ILogger<AccountService> _logger;

    public AccountService(AppDbContext context, ILogger<AccountService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<UserDto> Authenticate(string username, string password)
    {
        _logger.LogInformation("Attempting to authenticate user: {Username}", username);
        var user = await _context.Users.Include(u => u.Cart).ThenInclude(c => c.Tickets).ThenInclude(t => t.Showtime).ThenInclude(s => s.Movie).FirstOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            _logger.LogWarning("User not found: {Username}", username);
            return null;
        }

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            _logger.LogWarning("Invalid password for user: {Username}", username);
            return null;
        }

        // User has a cart
        var cart = await _context.Carts.Include(c => c.Tickets).ThenInclude(t => t.Showtime).ThenInclude(s => s.Movie).FirstOrDefaultAsync(c => c.UserId == user.Id);
        if (cart == null)
        {
            cart = new Cart
            {
                UserId = user.Id,
                User = user,
                Tickets = new List<Ticket>()
            };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        // Authentication successful
        _logger.LogInformation("User authenticated: {Username}", username);
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            DateOfBirth = user.DateOfBirth,
            Cart = new CartDto
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                Tickets = cart.Tickets.Select(t => new CartTicketDto
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
            }
        };
    }

    public async Task<UserDto> Register(string email, string username, string password, DateTime dateOfBirth)
    {
        _logger.LogInformation("Registering user: {Username}", username);
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var user = new User
        {
            Email = email,
            Username = username,
            Password = hashedPassword,
            DateOfBirth = dateOfBirth,
            Role = "User"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Create a cart for the user
        var cart = new Cart
        {
            UserId = user.Id,
            User = user,
            Tickets = new List<Ticket>()
        };
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();

        _logger.LogInformation("User registered: {Username}", username);
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            DateOfBirth = user.DateOfBirth,
            Cart = new CartDto
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                Tickets = cart.Tickets.Select(t => new CartTicketDto
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
            }
        };
    }

    public async Task<User> GetUserByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetUserById(string userId)
    {
        if (int.TryParse(userId, out int parsedUserId))
        {
            var user = await _context.Users
                .Include(u => u.PaymentDetails)
                .FirstOrDefaultAsync(u => u.Id == parsedUserId);
            return user ?? throw new InvalidOperationException("User not found");
        }
        return null;
    }

    public async Task<UserDto> UpdateUser(string userId, UserUpdateDto updateDto)
    {
        if (!int.TryParse(userId, out int parsedUserId))
        {
            return null;
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == parsedUserId);
        if (user == null)
        {
            return null;
        }

        user.Email = updateDto.Email;
        user.Username = updateDto.Username;
        user.DateOfBirth = updateDto.DateOfBirth;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return new UserDto
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
    }

    public async Task<(bool Success, string Message)> UpdatePassword(string userId, UserPasswordDto passwordUpdateDto)
    {
        if (!int.TryParse(userId, out int parsedUserId))
        {
            return (false, "Invalid User Id");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == parsedUserId);
        if (user == null || !BCrypt.Net.BCrypt.Verify(passwordUpdateDto.OldPassword, user.Password))
        {
            return (false, "Incorrect Old Password");
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(passwordUpdateDto.NewPassword);
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return (true, "Password Updated Successfully");
    }
}