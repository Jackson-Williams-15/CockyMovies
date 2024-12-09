using CM.API.Interfaces;
using CM.API.Models;
using CM.API.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CM.API.Services
{
    public class RatingService : IRatingService
    {
        private readonly AppDbContext _context;

        // Constructor to inject the database context (AppDbContext).
        public RatingService(AppDbContext context)
        {
            _context = context;
        }

        // Method to retrieve all ratings from the database.
        // Asynchronously fetches the list of ratings.
        public async Task<List<Rating>> GetRatings()
        {
            // Fetches all ratings from the Ratings table in the database.
            return await _context.Ratings.ToListAsync();
        }
    }
}
