using CM.API.Models;
using System.Collections.Generic;

namespace CM.API.Interfaces;

/// <summary>
/// Interface for genre service.
/// </summary>
public interface IGenreService
{
    /// <summary>
    /// Gets all genres.
    /// </summary>
    /// <returns>A list of genres.</returns>
    Task<List<Genre>> GetGenres();

    /// <summary>
    /// Gets a genre by its identifier.
    /// </summary>
    /// <param name="id">The genre identifier.</param>
    /// <returns>The genre with the specified identifier, or null if not found.</returns>
    Task<Genre?> GetGenreById(int id);

    /// <summary>
    /// Adds a new genre.
    /// </summary>
    /// <param name="genre">The genre entity.</param>
    /// <<returns>A boolean indicating whether the genre was added successfully.</returns>>
    Task<bool> AddGenre(Genre genre);
}
