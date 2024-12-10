using CM.API.Data;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CM.API.Services
{
    /// <summary>
    /// Service implementation for managing user accounts, including authentication, registration, and user updates.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context; // Database context
        private readonly ILogger<AccountService> _logger; // Logger for the service

        // Constructor that initializes the AccountService with necessary services
        public AccountService(AppDbContext context, ILogger<AccountService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Authenticates a user by verifying their username and password.
        /// </summary>
        /// <param name="username">The username of the user attempting to authenticate.</param>
        /// <param name="password">The password of the user attempting to authenticate.</param>
        /// <returns>A UserDto object if authentication is successful, otherwise null.</returns>
        public async Task<UserDto?> Authenticate(string username, string password)
        {
            _logger.LogInformation("Attempting to authenticate user: {Username}", username);
            
            // Retrieve the user and their cart details
            var user = _context.Users != null ? await _context.Users
                .Include(u => u.Cart)
                    .ThenInclude(c => c.Tickets)
                    .ThenInclude(t => t.Showtime)
                    .ThenInclude(s => s.Movie)
                .FirstOrDefaultAsync(u => u.Username == username) : null;

            if (user == null)
            {
                _logger.LogWarning("User not found: {Username}", username);
                return null;
            }

            // Verify the password using BCrypt
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                _logger.LogWarning("Invalid password for user: {Username}", username);
                return null;
            }

            // If user doesn't have a cart, create one
            var cart = await _context.Carts
                .Include(c => c.Tickets)
                    .ThenInclude(t => t.Showtime)
                    .ThenInclude(s => s.Movie)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);
            
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

            // Return the user data and cart information as UserDto
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
                                Rating = t.Showtime.Movie.Rating?.ToString() ?? string.Empty
                            }
                        }
                    }).ToList()
                }
            };
        }

        /// <summary>
        /// Registers a new user and creates a cart for them.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="dateOfBirth">The date of birth of the user.</param>
        /// <returns>The newly registered user as a UserDto.</returns>
        public async Task<UserDto> Register(string email, string username, string password, DateTime dateOfBirth)
        {
            _logger.LogInformation("Registering user: {Username}", username);
            
            // Hash the password before storing it
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // Create a new user entity
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

            // Return the user and cart details
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
                                Rating = t.Showtime.Movie.Rating?.ToString() ?? string.Empty
                            }
                        }
                    }).ToList()
                }
            };
        }

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The user if found, otherwise null.</returns>
        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The user if found, otherwise null.</returns>
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

        /// <summary>
        /// Updates a user's profile details.
        /// </summary>
        /// <param name="userId">The ID of the user to update.</param>
        /// <param name="updateDto">The DTO containing the updated user details.</param>
        /// <returns>The updated user as a UserDto, or null if the update failed.</returns>
        public async Task<UserDto?> UpdateUser(string userId, UserUpdateDto updateDto)
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

            // Update user fields if provided
            user.Email = updateDto.Email ?? user.Email;
            user.Username = updateDto.Username ?? user.Username;
            user.DateOfBirth = updateDto.DateOfBirth;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            // Return updated user data
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
                                Rating = t.Showtime.Movie.Rating?.ToString() ?? string.Empty
                            }
                        }
                    }).ToList()
                } : null
            };
        }

        /// <summary>
        /// Updates the user's password.
        /// </summary>
        /// <param name="userId">The ID of the user whose password is being updated.</param>
        /// <param name="passwordUpdateDto">The DTO containing the old and new passwords.</param>
        /// <returns>A tuple indicating whether the update was successful and a message.</returns>
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

            // Hash the new password and update the user's record
            user.Password = BCrypt.Net.BCrypt.HashPassword(passwordUpdateDto.NewPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return (true, "Password Updated Successfully");
        }
    }
}
