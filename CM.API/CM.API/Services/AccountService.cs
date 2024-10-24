using System.Linq;
using System.Threading.Tasks;
using CM.API.Data;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CM.API.Services;

    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;

        public LoginService(AppDbContext context)
        {
            _context = context;
        }

        public async User Authenticate(string username, string password)
        {
            var user = await _context.Users
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefaultAsync();

            // Return null if user not found
            if (user == null)
                return null;

            // Authentication successful
            return user;
        }
    }
