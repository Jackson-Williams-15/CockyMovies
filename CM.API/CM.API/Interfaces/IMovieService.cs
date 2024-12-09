using CM.API.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CM.API.Interfaces;
/// <summary>
/// Interface for movie service operations.
/// </summary>
public interface IMovieService
{
    /// <summary>
    /// Gets a movie by its ID.
    /// </summary>
    /// <param name="id">The movie ID.</param>
    /// <seealso cref="CM.API.Services.MovieService.GetMovieById(int)"/>
    Task<Movie?> GetMovieById(int id);

    /// <summary>
    /// Adds a new movie.
    /// </summary>
    /// <param name="movie">The movie to add.</param>
    /// <seealso cref="CM.API.Services.MovieService.AddMovie(Movie)"/>
    Task<bool> AddMovie(Movie movie);

    /// <summary>
    /// Removes a movie.
    /// </summary>
    /// <param name="movie">The movie to remove.</param>
    /// <seealso cref="CM.API.Services.MovieService.RemoveMovie(Movie)"/>
    Task<bool> RemoveMovie(Movie movie);

    /// <summary>
    /// Edits an existing movie.
    /// </summary>
    /// <param name="oldMovie">The existing movie.</param>
    /// <param name="newMovie">The new movie data.</param>
    /// <seealso cref="CM.API.Services.MovieService.EditMovie(Movie, Movie)"/>
    Task<bool> EditMovie(Movie oldMovie, Movie newMovie);

    /// <summary>
    /// Gets a list of movies, optionally filtered by genre and rating IDs.
    /// </summary>
    /// <param name="genreIds">Optional list of genre IDs to filter movies.</param>
    /// <param name="ratingIds">Optional list of rating IDs to filter movies.</param>
    /// <seealso cref="CM.API.Services.MovieService.GetMovies(List{int}?, List{int}?)"/>
    Task<List<Movie>> GetMovies(List<int>? genreIds = null, List<int>? ratingIds = null);

    /// <summary>
    /// Gets a list of genres by their IDs.
    /// </summary>
    /// <param name="genreIds">The list of genre IDs.</param>
    /// <seealso cref="CM.API.Services.MovieService.GetGenresByIds(List{int})"/>
    Task<List<Genre>> GetGenresByIds(List<int> genreIds);
}