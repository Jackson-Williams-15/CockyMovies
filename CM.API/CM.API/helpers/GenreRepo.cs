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
        private Dictionary<int, Genre> _genresDictionary;

        public GenreRepository(AppDbContext context)
        {
            _context = context;
            _genresDictionary = new Dictionary<int, Genre>();
        }

        public async Task LoadGenres()
        {
            var genres = await _context.Genres.ToListAsync();
            _genresDictionary = genres.ToDictionary(g => g.Id);
        }

        public async Task<List<Genre>> GetGenres()
        {
            if (!_genresDictionary.Any())
            {
                await LoadGenres();
            }
            return _genresDictionary.Values.ToList();
        }

        public async Task<Genre?> GetGenreById(int id)
        {
            if (!_genresDictionary.Any())
            {
                await LoadGenres();
            }
            return _genresDictionary.TryGetValue(id, out var genre) ? genre : null;
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
            _genresDictionary[genre.Id] = genre;
            return true;
        }
    }
}