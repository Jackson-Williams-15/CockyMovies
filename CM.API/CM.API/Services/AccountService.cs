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

    public async Task<User> Authenticate(string username, string password)
    {
        _logger.LogInformation("Attempting to authenticate user: {Username}", username);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

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

        // Authentication successful
        _logger.LogInformation("User authenticated: {Username}", username);
        return user;
    }

    public async Task<User> Register(string email, string username, string password, DateTime dateOfBirth)
    {
        _logger.LogInformation("Registering user: {Username}", username);
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var user = new User
        {
            Email = email,
            Username = username,
            Password = hashedPassword,
            DateOfBirth = dateOfBirth
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        _logger.LogInformation("User registered: {Username}", username);
        return user;
    }

    public async Task<User> GetUserByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User> GetUserById(string userId)
    {
        if (int.TryParse(userId, out int parsedUserId))
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == parsedUserId);
        }
        return null;
    }
}