using CM.API.Data;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CM.API.Services;

// AccountService handles authentication, registration, and user management logic.
public class AccountService : IAccountService
{
    private readonly AppDbContext _context;  // DbContext for database interaction
    private readonly ILogger<AccountService> _logger;  // Logger for logging events

    // Constructor for dependency injection of AppDbContext and ILogger
    public AccountService(AppDbContext context, ILogger<AccountService> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Method for authenticating users
    public async Task<UserDto> Authenticate(string username, string password)
    {
        // Log the authentication attempt
        _logger.LogInformation("Attempting to authenticate user: {Username}", username);

        // Retrieve the user from the database, including related cart and tickets
        var user = await _context.Users
            .Include(u => u.Cart)  // Include the user's cart
            .ThenInclude(c => c.Tickets)  // Include the tickets in the cart
            .ThenInclude(t => t.Showtime)  // Include the showtimes for the tickets
            .ThenInclude(s => s.Movie)  // Include the movie information for the showtime
            .FirstOrDefaultAsync(u => u.Username == username);  // Fetch user by username

        // If user is not found, log a warning and return null
        if (user == null)
        {
            _logger.LogWarning("User not found: {Username}", username);
            return null;
        }

        // Verify the password using bcrypt hashing
        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            _logger.LogWarning("Invalid password for user: {Username}", username);
            return null;
        }

        // If the user has a cart, retrieve it, otherwise create a new one
        var cart = await _context.Carts
            .Include(c => c.Tickets)
            .ThenInclude(t => t.Showtime)
            .ThenInclude(s => s.Movie)
            .FirstOrDefaultAsync(c => c.UserId == user.Id);

        // If no cart exists, create a new one for the user
        if (cart == null)
        {
            cart = new Cart
            {
                UserId = user.Id,
                User = user,
                Tickets = new List<Ticket>()  // Initialize an empty list of tickets
            };
            _context.Carts.Add(cart);  // Add the cart to the context
            await _context.SaveChangesAsync();  // Save changes to the database
        }

        // Authentication successful, log the event and return the UserDto
        _logger.LogInformation("User authenticated: {Username}", username);
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            DateOfBirth = user.DateOfBirth,
            Role = user.Role,
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

    // Method to register a new user
    public async Task<UserDto> Register(string email, string username, string password, DateTime dateOfBirth)
    {
        // Log the registration attempt
        _logger.LogInformation("Registering user: {Username}", username);

        // Hash the user's password before storing it
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        // Create a new User entity with the provided data
        var user = new User
        {
            Email = email,
            Username = username,
            Password = hashedPassword,
            DateOfBirth = dateOfBirth,
            Role = "User"
        };

        _context.Users.Add(user);  // Add the user to the context
        await _context.SaveChangesAsync();  // Save the user to the database

        // Create a cart for the newly registered user
        var cart = new Cart
        {
            UserId = user.Id,
            User = user,
            Tickets = new List<Ticket>()  // Initialize an empty list of tickets
        };
        _context.Carts.Add(cart);  // Add the cart to the context
        await _context.SaveChangesAsync();  // Save the cart to the database

        // Log successful registration and return UserDto
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

    // Method to get user by username
    public async Task<User> GetUserByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);  // Fetch user by username
    }

    // Method to get user by userId (as a string, to handle different formats)
    public async Task<User?> GetUserById(string userId)
    {
        // Try to parse the userId to an integer
        if (int.TryParse(userId, out int parsedUserId))
        {
            var user = await _context.Users
                .Include(u => u.PaymentDetails)  // Include payment details if they exist
                .FirstOrDefaultAsync(u => u.Id == parsedUserId);  // Fetch user by ID
            return user ?? throw new InvalidOperationException("User not found");  // Return user or throw exception if not found
        }
        return null;  // Return null if userId cannot be parsed
    }

    // Method to update user information
    public async Task<UserDto> UpdateUser(string userId, UserUpdateDto updateDto)
    {
        // Try to parse the userId to an integer
        if (!int.TryParse(userId, out int parsedUserId))
        {
            return null;  // Return null if userId is invalid
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == parsedUserId);  // Fetch user by ID
        if (user == null)
        {
            return null;  // Return null if user is not found
        }

        // Update the user's properties with the new data from the updateDto
        user.Email = updateDto.Email;
        user.Username = updateDto.Username;
        user.DateOfBirth = updateDto.DateOfBirth;

        _context.Users.Update(user);  // Mark the user entity as modified
        await _context.SaveChangesAsync();  // Save the changes to the database

        // Return the updated user as a UserDto
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
            } : null  // Return cart details if available, otherwise return null
        };
    }

    // Method to update a user's password
    public async Task<(bool Success, string Message)> UpdatePassword(string userId, UserPasswordDto passwordUpdateDto)
    {
        // Try to parse the userId to an integer
        if (!int.TryParse(userId, out int parsedUserId))
        {
            return (false, "Invalid User Id");  // Return error if userId is invalid
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == parsedUserId);  // Fetch user by ID
        // Return error if user is not found or old password doesn't match
        if (user == null || !BCrypt.Net.BCrypt.Verify(passwordUpdateDto.OldPassword, user.Password))
        {
            return (false, "Incorrect Old Password");
        }

        // Hash the new password and update the user's password
        user.Password = BCrypt.Net.BCrypt.HashPassword(passwordUpdateDto.NewPassword);
        _context.Users.Update(user);  // Mark the user entity as modified
        await _context.SaveChangesAsync();  // Save the changes to the database

        // Return success message upon successful password update
        return (true, "Password Updated Successfully");
    }
}
