using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CM.API.Models;
using CM.API.Data;
using Microsoft.EntityFrameworkCore;

namespace CM.API.Repositories
{
    public class RatingsRepository
    {
        private readonly AppDbContext _context;
        private Dictionary<int, Rating> _ratingsDictionary;

        public RatingsRepository(AppDbContext context)
        {
            _context = context;
            _ratingsDictionary = new Dictionary<int, Rating>();
        }

        public async Task LoadRatings()
        {
            var ratings = await _context.Ratings.ToListAsync();
            _ratingsDictionary = ratings.ToDictionary(g => g.Id);
        }

        public async Task<List<Rating>> GetRatings()
        {
            if (!_ratingsDictionary.Any())
            {
                await LoadRatings();
            }
            return _ratingsDictionary.Values.ToList();
        }

        public async Task<Rating?> GetRatingsById(int id)
        {
            if (!_ratingsDictionary.Any())
            {
                await LoadRatings();
            }
            return _ratingsDictionary.TryGetValue(id, out var ratings) ? ratings : null;
        }

        public async Task<bool> AddRating(Rating rating)
        {
            // Genre already exists
            if (await _context.Ratings.AnyAsync(g => g.Name.ToLower() == rating.Name.ToLower()))
            {
                return false;
            }

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            _ratingsDictionary[rating.Id] = rating;
            return true;
        }
    }
}