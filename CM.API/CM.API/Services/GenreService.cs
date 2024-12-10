using CM.API.Interfaces;
using CM.API.Models;
using System.Collections.Generic;
using System.Linq;
using CM.API.Repositories;
namespace CM.API.Services;

/// <summary>
/// Service for managing genres.
/// </summary>
public class GenreService : IGenreService
{
    
    private readonly GenreRepository _genreRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenreService"/> class.
    /// </summary>
    /// <param name="genreRepository">The genre repository.</param>
    public GenreService(GenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    /// <summary>
    /// Gets all genres.
    /// </summary>
    /// <returns>A list of genres.</returns>
    public async Task<List<Genre>> GetGenres()
    {
        return await _genreRepository.GetGenres();
    }

    /// <summary>
    /// Gets a genre by its identifier.
    /// </summary>
    /// <param name="id">The genre identifier.</param>
    /// <returns>The genre with the specified identifier, or null if not found.</returns>
    public async Task<Genre?> GetGenreById(int id)
    {
        return await _genreRepository.GetGenreById(id);
    }

    /// <summary>
    /// Adds a new genre.
    /// </summary>
    /// <param name="genre">The genre entity.</param>
    /// <returns>A boolean indicating whether the genre was added successfully.</returns>
    public async Task<bool> AddGenre(Genre genre)
    {
        return await _genreRepository.AddGenre(genre);
    }
}

