using CM.API.Interfaces;
using CM.API.Models;
using CM.API.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CM.API.Services;

/// <summary>
/// Service for managing showtimes.
/// </summary>
public class ShowtimeService : IShowtimeService
{
    private readonly AppDbContext _context;
    private readonly IMovieService _movieService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShowtimeService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="movieService">The movie service.</param>
    public ShowtimeService(AppDbContext context, IMovieService movieService)
    {
        _context = context;
        _movieService = movieService;
    }

    /// <summary>
    /// Adds a new showtime.
    /// </summary>
    /// <param name="showtime">The showtime to add.</param>
    /// <returns>A task with a result indicating success or failure of adding a showtime.</returns>
    public async Task<bool> AddShowtime(Showtime showtime)
    {
        if (await _context.Showtime.AnyAsync(s => s.Id == showtime.Id))
        {
            return false;
        }

        // Set the initial number of available tickets
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

    /// <summary>
    /// Removes a showtime by ID.
    /// </summary>
    /// <param name="id">The ID of the showtime to remove.</param>
    /// <returns>A task with a result indicating success or failure of removing a showtime.</returns>
    public async Task<bool> RemoveShowtime(int id)
    {
        // Fetch showtime from DB by id
        var showtime = await _context.Showtime.FindAsync(id);
        if (showtime == null)
        {
            return false; // Showtime not found
        }

        // remove showtime
        _context.Showtime.Remove(showtime);
        await _context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Edits an existing showtime.
    /// </summary>
    /// <param name="id">The ID of the showtime to edit.</param>
    /// <param name="editedShowtime">The edited showtime object.</param>
    /// <returns>A task representing with aresult indicating success or failure of editing the showtime.</returns>
    public async Task<bool> EditShowtime(int id, Showtime editedShowtime)
    {
        // Find the showtime with tickets
        var existingShowtime = await _context.Showtime
            .Include(s => s.Tickets)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (existingShowtime == null)
        {
            return false; // Showtime not found
        }

        // Update the fields
        existingShowtime.StartTime = editedShowtime.StartTime;
        existingShowtime.MovieId = editedShowtime.MovieId;

        // Check if capacity has changed
        if (editedShowtime.Capacity != existingShowtime.Capacity)
        {
            if (editedShowtime.Capacity < existingShowtime.TicketsAvailable)
            {
                throw new InvalidOperationException("New capacity cannot be less than the number of tickets available.");
            }

            // Update the capacity and adjust the number of available tickets
            existingShowtime.Capacity = editedShowtime.Capacity;
            existingShowtime.TicketsAvailable = editedShowtime.Capacity - (existingShowtime.Capacity - existingShowtime.TicketsAvailable);

        }

        // try and catch to update the showtime in the DB, throws exception if fails
        try
        {
            _context.Showtime.Update(existingShowtime);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // Log exception
            Console.WriteLine($"Error editing showtime: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Gets showtimes by movie ID.
    /// </summary>
    /// <param name="movieId">The movie ID.</param>
    /// <returns>S list of showtime DTOs.</returns>
    public async Task<List<ShowtimeDto>> GetShowtimesByMovieId(int movieId)
    {
        // Fetch showtimes by the movie ID, includes the movie and tickets
        var showtimes = await _context.Showtime
            .Include(s => s.Tickets)
            .Include(s => s.Movie)
            .Where(s => s.MovieId == movieId)
            .ToListAsync();
        
        // Map to DTO
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