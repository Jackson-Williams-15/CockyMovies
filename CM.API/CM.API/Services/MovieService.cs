using CM.API.Interfaces;
using CM.API.Models;
using CM.API.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CM.API.Repositories;
using System.Threading.Tasks;

namespace CM.API.Services;

// MovieService class implements the IMovieService interface and provides
// methods for managing movies, such as adding, removing, editing, and retrieving movies.
public class MovieService : IMovieService
{
    // Database context for accessing the Movies table.
    private readonly AppDbContext _context;
    // Repository to fetch Genre data.
    private readonly GenreRepository _genreRepository;

    // Constructor initializes MovieService with AppDbContext and GenreRepository dependencies.
    public MovieService(AppDbContext context, GenreRepository genreRepository)
    {
        _context = context;
        _genreRepository = genreRepository;
    }

    // Method to add a new movie to the database.
    public async Task<bool> AddMovie(Movie movie)
    {
        // Check if the movie already exists in the database by its Id.
        if (_context.Movies.Any(m => m.Id == movie.Id))
        {
            return false; // Return false if the movie already exists.
        }

        // If no image URL is provided, set a default image URL.
        if (string.IsNullOrWhiteSpace(movie.ImageUrl))
        {
            movie.ImageUrl = "https://plus.unsplash.com/premium_vector-1682303466154-2161da750ac7?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTl8fG1vdmllfGVufDB8fDB8fHww";
        }

        // Add the movie to the Movies table.
        _context.Movies.Add(movie);

        // Save the changes asynchronously to the database.
        await _context.SaveChangesAsync();
        // Return true indicating that the movie was successfully added.
        return true;
    }

    // Method to remove a movie from the database.
    public async Task<bool> RemoveMovie(Movie movie)
    {
        // Find the movie by its Id in the database.
        var existingMovie = await _context.Movies.FindAsync(movie.Id);

        // If the movie doesn't exist, return false.
        if (existingMovie == null)
        {
            return false;
        }

        // Remove the movie from the Movies table.
        _context.Movies.Remove(existingMovie);

        // Save the changes asynchronously to the database.
        await _context.SaveChangesAsync();
        // Return true indicating that the movie was successfully removed.
        return true;
    }

    // Method to update an existing movie with new data.
    public async Task<bool> EditMovie(Movie oldMovie, Movie newMovie)
    {
        // Retrieve the existing movie with its related genres.
        var existingMovie = await _context.Movies
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Id == oldMovie.Id);

        // If the movie is not found, return false.
        if (existingMovie == null)
        {
            return false;
        }

        // Update the fields of the existing movie with the new values.
        existingMovie.Title = newMovie.Title;
        existingMovie.Description = newMovie.Description;
        existingMovie.DateReleased = newMovie.DateReleased;
        existingMovie.ImageUrl = newMovie.ImageUrl ?? existingMovie.ImageUrl;
        existingMovie.RatingId = newMovie.RatingId;

        // If new genres are provided, update the genres of the movie.
        if (newMovie.Genres != null && newMovie.Genres.Any())
        {
            existingMovie.Genres.Clear();
            foreach (var genre in newMovie.Genres)
            {
                existingMovie.Genres.Add(genre);
            }
        }

        // Save the updated movie to the database.
        await _context.SaveChangesAsync();
        // Return true indicating that the movie was successfully edited.
        return true;
    }

    // Method to get a movie by its Id.
    public async Task<Movie?> GetMovieById(int id)
    {
        // Retrieve the movie with related genres, showtimes, tickets, and rating.
        return await _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.Showtimes)
                .ThenInclude(s => s.Tickets)
            .Include(m => m.Rating)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    // Method to retrieve a list of movies with optional filters by genre and rating.
    public async Task<List<Movie>> GetMovies(List<int>? genreIds = null, List<int>? ratingIds = null)
    {
        // Build a query that includes related genres, showtimes, tickets, and rating.
        var query = _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.Showtimes)
                .ThenInclude(s => s.Tickets)
            .Include(m => m.Rating)
            .AsQueryable();

        // Filter by genre if genreIds are provided.
        if (genreIds != null && genreIds.Any())
        {
            query = query.Where(m => m.Genres.Any(g => genreIds.Contains(g.Id)));
        }

        // Filter by rating if ratingIds are provided.
        if (ratingIds != null && ratingIds.Any())
        {
            query = query.Where(m => ratingIds.Contains(m.RatingId));
        }

        // Execute the query and return the list of movies.
        return await query.ToListAsync();
    }

    // Method to retrieve genres based on a list of genre IDs.
    public async Task<List<Genre>> GetGenresByIds(List<int> genreIds)
    {
        // Log the provided genre IDs for debugging purposes.
        System.Console.WriteLine("Provided genre IDs: " + string.Join(", ", genreIds));

        // Retrieve all genres from the genre repository.
        var genres = await _genreRepository.GetGenres();

        // Filter the genres based on the provided genreIds.
        var fetchedGenres = genres
            .Where(g => genreIds.Contains(g.Id))
            .ToList();

        // Log the fetched genres for debugging purposes.
        System.Console.WriteLine("Fetched Genres: " + string.Join(", ", fetchedGenres.Select(g => g.Name)));

        // Return the list of fetched genres.
        return fetchedGenres;
    }
}
