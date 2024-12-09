using CM.API.Models;  // Models like Genre
using System.Collections.Generic;  // Collection types (e.g., List<T>)

namespace CM.API.Interfaces
{
    // Interface defining genre-related service methods
    public interface IGenreService
    {
        // Get all genres
        Task<List<Genre>> GetGenres();

        // Get a genre by its ID
        Task<Genre?> GetGenreById(int id);

        // Add a new genre
        Task<bool> AddGenre(Genre genre);
    }
}
