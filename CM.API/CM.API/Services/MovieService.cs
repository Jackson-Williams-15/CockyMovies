using CM.API.Interfaces;
using CM.API.Models;
using CM.API.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CM.API.Repositories;
using System.Threading.Tasks;

namespace CM.API.Services;
public class MovieService : IMovieService
{
    private readonly AppDbContext _context;
    private readonly GenreRepository _genreRepository;

    public MovieService(AppDbContext context, GenreRepository genreRepository)
    {
        _context = context;
        _genreRepository = genreRepository;
    }

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
            existingMovie.Genres.Clear();
            foreach (var genre in newMovie.Genres)
            {
                existingMovie.Genres.Add(genre);
            }
        }

        // Save changes
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Movie?> GetMovieById(int id)
    {
        return await _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.Showtimes)
            .ThenInclude(s => s.Tickets)
            .Include(m => m.Rating)
            .Include(m => m.Reviews)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<Movie>> GetMovies(List<int>? genreIds = null, List<int>? ratingIds = null)
    {
        var query = _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.Showtimes)
            .ThenInclude(s => s.Tickets)
            .Include(m => m.Rating)
            .Include(m => m.Reviews)
            .AsQueryable();

        if (genreIds != null && genreIds.Any())
        {
            query = query.Where(m => m.Genres.Any(g => genreIds.Contains(g.Id)));
        }

        if (ratingIds != null && ratingIds.Any())
        {
            query = query.Where(m => ratingIds.Contains(m.RatingId));
        }

        return await query.ToListAsync();
    }

    public async Task<List<Genre>> GetGenresByIds(List<int> genreIds)
    {
        // Show in console the genre IDs 
        System.Console.WriteLine("Provided genre IDs: " + string.Join(", ", genreIds));

        var genres = await _genreRepository.GetGenres();
        var fetchedGenres = genres
            .Where(g => genreIds.Contains(g.Id))
            .ToList();

        // Showing genre IDs being fetched for the movie create
        System.Console.WriteLine("Fetched Genres: " + string.Join(", ", fetchedGenres.Select(g => g.Name)));

        return fetchedGenres;
    }
}