using CM.API.Interfaces;
using CM.API.Models;
using CM.API.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CM.API.Repositories;
using System.Threading.Tasks;

namespace CM.API.Services;

/// <summary>
/// Service for managing movies.
/// </summary>
public class MovieService : IMovieService
{
    private readonly AppDbContext _context;
    private readonly GenreRepository _genreRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="MovieService"/> class.
    /// </summary>
    /// <param name="context">The application database context.</param>
    /// <param name="genreRepository">The Genre repo 
    /// <see cref="CM.API.Repositories.GenreRepository"/></param>
    public MovieService(AppDbContext context, GenreRepository genreRepository)
    {
        _context = context;
        _genreRepository = genreRepository;
    }

    /// <summary>
    /// Adds a new movie.
    /// </summary>
    /// <param name="movie">The movie to add.</param>
    /// <returns>A boolean indicating the success or failure of adding a movie.</returns>
    public async Task<bool> AddMovie(Movie movie)
    {
        // if movie already exists
        if (_context.Movies.Any(m => m.Id == movie.Id))
        {
            return false;
        }

        //Set the ImageUrl while adding the movie
        if (string.IsNullOrWhiteSpace(movie.ImageUrl))
        {
            movie.ImageUrl = "https://plus.unsplash.com/premium_vector-1682303466154-2161da750ac7?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTl8fG1vdmllfGVufDB8fDB8fHww";
        }

        // add movie to the database
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Removes a movie.
    /// </summary>
    /// <param name="movie">The movie to remove.</param>
    /// <returns>A boolean indicating the success or failure of removing a movie.</returns>
    public async Task<bool> RemoveMovie(Movie movie)
    {
        var existingMovie = await _context.Movies.FindAsync(movie.Id);
        if (existingMovie == null)
        {
            return false;
        }

        _context.Movies.Remove(existingMovie);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Edits an existing movie.
    /// </summary>
    /// <param name="oldMovie">The existing movie.</param>
    /// <param name="newMovie">The new movie data.</param>
    /// <returns>A boolean indicating the success or failure of editing a movie.</returns>
    public async Task<bool> EditMovie(Movie oldMovie, Movie newMovie)
    {
        var existingMovie = await _context.Movies
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Id == oldMovie.Id);

        if (existingMovie == null)
        {
            return false; // Movie not found
        }

        // Update fields
        existingMovie.Title = newMovie.Title;
        existingMovie.Description = newMovie.Description;
        existingMovie.DateReleased = newMovie.DateReleased;
        existingMovie.ImageUrl = newMovie.ImageUrl ?? existingMovie.ImageUrl;
        existingMovie.RatingId = newMovie.RatingId;

        // Update genres if provided
        if (newMovie.Genres != null && newMovie.Genres.Any())
        {
            existingMovie.Genres?.Clear();
            foreach (var genre in newMovie.Genres)
            {
                existingMovie.Genres?.Add(genre);
            }
        }

        // Save changes
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Gets a movie by its ID.
    /// </summary>
    /// <param name="id">The movie ID.</param>
    /// <returns>The movie with the specified ID.</returns>
    public async Task<Movie?> GetMovieById(int id)
    {
        // Fetch the movie by ID and include related entities
        return await _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.Showtimes)
            .ThenInclude(s => s.Tickets)
            .Include(m => m.Rating)
            .Include(m => m.Reviews)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    /// <summary>
    /// Gets a list of movies, optionally filtered by the genre and rating IDs.
    /// </summary>
    /// <param name="genreIds">Optional list of genre IDs to filter movies.</param>
    /// <param name="ratingIds">Optional list of rating IDs to filter movies.</param>
    /// <returns>A list of movies.</returns>
    public async Task<List<Movie>> GetMovies(List<int>? genreIds = null, List<int>? ratingIds = null)
    {
        // Linq query for movies and related entities
        var query = _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.Showtimes)
            .ThenInclude(s => s.Tickets)
            .Include(m => m.Rating)
            .Include(m => m.Reviews)
            .AsQueryable();

        // Filter by genre IDs
        if (genreIds != null && genreIds.Any())
        {
            query = query.Where(m => m.Genres != null && m.Genres.Any(g => genreIds.Contains(g.Id)));
        }

        // Filter by rating IDs
        if (ratingIds != null && ratingIds.Any())
        {
            query = query.Where(m => ratingIds.Contains(m.RatingId));
        }
        // Execute the query
        return await query.ToListAsync();
    }

    /// <summary>
    /// Gets a list of genres by their IDs.
    /// </summary>
    /// <param name="genreIds">The list of genre IDs.</param>
    /// <returns>A list of genres.</returns>
    public async Task<List<Genre>> GetGenresByIds(List<int> genreIds)
    {
        // Show in console the genre IDs 
        System.Console.WriteLine("Provided genre IDs: " + string.Join(", ", genreIds));
        // Get genres from the genre repo
        var genres = await _genreRepository.GetGenres();
        // Filter genres by their ids
        var fetchedGenres = genres
            .Where(g => genreIds.Contains(g.Id))
            .ToList();

        // Showing genre IDs being fetched for the movie create
        System.Console.WriteLine("Fetched Genres: " + string.Join(", ", fetchedGenres.Select(g => g.Name)));

        return fetchedGenres;
    }
}