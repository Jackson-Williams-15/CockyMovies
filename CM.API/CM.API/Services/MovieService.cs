using CM.API.Interfaces;
using CM.API.Models;
using CM.API.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CM.API.Repositories;
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

    public bool AddMovie(Movie movie)
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
        _context.SaveChanges();
        return true;
    }

    public Movie? GetMovieById(int id)
    {
        var movie = _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.Showtimes)
            .ThenInclude(s => s.Tickets)
            .FirstOrDefault(m => m.Id == id);

        return movie;
    }

    public List<Movie> GetMovies()
    {
        return _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.Showtimes)
            .ThenInclude(s => s.Tickets)
            .ToList();
    }

    public List<Genre> GetGenresByIds(List<int> genreIds)
    {
        // Show in console the genre IDs 
        System.Console.WriteLine("Provided genre IDs: " + string.Join(", ", genreIds));

        var fetchedGenres = _genreRepository.GetGenres()
            .Where(g => genreIds.Contains(g.Id))
            .ToList();

        // Showing genre IDs being fetched for the movie create
        System.Console.WriteLine("Fetched Genres: " + string.Join(", ", fetchedGenres.Select(g => g.Name)));

        return fetchedGenres;
    }
}