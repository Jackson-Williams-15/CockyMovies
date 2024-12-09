using CM.API.Interfaces;  // Interfaces for service layer
using CM.API.Models;      // Application models like Genre
using System.Collections.Generic;  // Collection types (e.g., List<T>)
using System.Linq;        // LINQ (not used here but might be useful)
using CM.API.Repositories;  // Repository classes for data access

namespace CM.API.Services
{
    // Service class for genre-related operations
    public class GenreService : IGenreService
    {
        // Repository for accessing genre data
        private readonly GenreRepository _genreRepository;

        // Constructor to inject the repository
        public GenreService(GenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        // Get all genres
        public async Task<List<Genre>> GetGenres()
        {
            return await _genreRepository.GetGenres();
        }

        // Get a genre by its ID
        public async Task<Genre?> GetGenreById(int id)
        {
            return await _genreRepository.GetGenreById(id);
        }

        // Add a new genre
        public async Task<bool> AddGenre(Genre genre)
        {
            return await _genreRepository.AddGenre(genre);
        }
    }
}
