using CM.API.Interfaces;
using CM.API.Models;
using CM.API.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> AddShowtime(Showtime showtime)
    {
        if (await _context.Showtime.AnyAsync(s => s.Id == showtime.Id))
        {
            return false;
        }

        showtime.TicketsAvailable = showtime.Capacity;
        Console.WriteLine($"Showtime created with ID: {showtime.Id}, Capacity: {showtime.Capacity}, TicketsAvailable: {showtime.TicketsAvailable}");

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
        await _context.SaveChangesAsync();

        // Update movie's showtimes list
        var movie = await _movieService.GetMovieById(showtime.MovieId);
        if (movie != null)
        {
            movie.Showtimes.Add(showtime);
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        return true;
    }

    public async Task<List<ShowtimeDto>> GetShowtimesByMovieId(int movieId)
    {
        var showtimes = await _context.Showtime
            .Include(s => s.Tickets)
            .Include(s => s.Movie)
            .Where(s => s.MovieId == movieId)
            .ToListAsync();

        return showtimes.Select(s => new ShowtimeDto
        {
            Id = s.Id,
            StartTime = s.StartTime,
            Tickets = s.Tickets.Select(t => new TicketDto
            {
                Id = t.Id,
                Price = t.Price
            }).ToList(),
            AvailableTickets = s.Capacity - s.Tickets.Count,
            Movie = s.Movie != null ? new MovieDto
            {
                Id = s.Movie.Id,
                Title = s.Movie.Title,
                Description = s.Movie.Description,
                DateReleased = s.Movie.DateReleased,
                Rating = s.Movie.Rating?.ToString() ?? "Unrated"
            } : null
        }).ToList();
    }
}