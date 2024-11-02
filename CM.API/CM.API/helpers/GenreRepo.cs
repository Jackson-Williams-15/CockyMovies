using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CM.API.Models;
using CM.API.Data;
using Microsoft.EntityFrameworkCore;

namespace CM.API.Repositories
{
    public class GenreRepository
    {
        private readonly AppDbContext _context;

        public GenreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> GetGenres()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre?> GetGenreById(int id)
        {
            return await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<bool> AddGenre(Genre genre)
        {
            // Genre already exists
            if (await _context.Genres.AnyAsync(g => g.Name.ToLower() == genre.Name.ToLower()))
            {
                return false;
            }

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}