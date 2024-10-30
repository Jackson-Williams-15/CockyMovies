using CM.API.Interfaces;
using CM.API.Models;
using CM.API.Data;
using System.Collections.Generic;
using System.Linq;

namespace CM.API.Services;
public class ShowtimeService : IShowtimeService
{
    private readonly AppDbContext _context;
    private readonly IMovieService _movieService;

    public ShowtimeService(AppDbContext context, IMovieService movieService)
    {
        _context = context;
        _movieService = movieService;
    }

    public bool AddShowtime(Showtime showtime)
    {
        if (_context.Showtime.Any(s => s.Id == showtime.Id))
        {
            return false;
        }

        // Create tickets based on the capacity of the showtime
        for (int i = 0; i < showtime.Capacity; i++)
        {
            showtime.Tickets.Add(new Ticket
            {
                Price = 10.00m, // default price
                Showtime = showtime
            });
        }

        _context.Showtime.Add(showtime);
        _context.SaveChanges();

        // Update movie's showtimes list
        var movie = _movieService.GetMovieById(showtime.MovieId);
        if (movie != null)
        {
            movie.Showtimes.Add(showtime);
            _context.Movies.Update(movie);
            _context.SaveChanges();
        }

        return true;
    }

    public List<Showtime> GetShowtimesByMovieId(int movieId)
    {
        return _context.Showtime.Where(s => s.MovieId == movieId).ToList();
    }
}