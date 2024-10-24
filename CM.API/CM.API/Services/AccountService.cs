using CM.API.Data;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BCrypt.Net;

namespace CM.API.Services;

    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _context.Users
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();

            // Return null if user not found or password does not match
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;

            // Authentication successful
            return user;
        }

        public async Task<User> Register(string email, string username, string password, DateTime dateOfBirth)
        {
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

            return user;
        }
    }