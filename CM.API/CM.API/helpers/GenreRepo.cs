using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CM.API.Models;
using CM.API.Data;
using Microsoft.EntityFrameworkCore;

namespace CM.API.Repositories
{
    /// <summary>
    /// Repository for managing genres.
    /// </summary>
    public class GenreRepository
    {
        private readonly AppDbContext _context;
        private Dictionary<int, Genre> _genresDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenreRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public GenreRepository(AppDbContext context)
        {
            _context = context;
            _genresDictionary = new Dictionary<int, Genre>();
        }

        /// <summary>
        /// Loads all genres from the database into the dictionary.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task LoadGenres()
        {
            var genres = await _context.Genres.ToListAsync();
            _genresDictionary = genres.ToDictionary(g => g.Id);
        }

        /// <summary>
        /// Gets all genres.
        /// </summary>
        /// <returns>A list of genres.</returns>
        public async Task<List<Genre>> GetGenres()
        {
            if (!_genresDictionary.Any())
            {
                await LoadGenres();
            }
            return _genresDictionary.Values.ToList();
        }

        /// <summary>
        /// Gets a genre by its identifier.
        /// </summary>
        /// <param name="id">The genre identifier.</param>
        /// <returns>The genre with the specified identifier, or null if not found.</returns>
        public async Task<Genre?> GetGenreById(int id)
        {
            if (!_genresDictionary.Any())
            {
                await LoadGenres();
            }
            return _genresDictionary.TryGetValue(id, out var genre) ? genre : null;
        }

        /// <summary>
        /// Adds a new genre.
        /// </summary>
        /// <param name="genre">The genre entity.</param>
        /// <returns>A boolean indicating whether the genre was added successfully.</returns>
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