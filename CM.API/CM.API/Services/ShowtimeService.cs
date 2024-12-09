using CM.API.Interfaces;  // Interfaces for service layer
using CM.API.Models;      // Models like Showtime, Ticket, etc.
using CM.API.Data;        // Database context (AppDbContext)
using System.Collections.Generic;  // Collection types like List<T>
using System.Linq;        // LINQ functions for querying
using System.Threading.Tasks;  // Asynchronous programming
using Microsoft.EntityFrameworkCore;  // EF Core for database operations

namespace CM.API.Services
{
    public class ShowtimeService : IShowtimeService
    {
        // Database context for accessing the database
        private readonly AppDbContext _context;

        // Movie service for managing movie-related operations
        private readonly IMovieService _movieService;

        // Constructor to inject dependencies (AppDbContext and IMovieService)
        public ShowtimeService(AppDbContext context, IMovieService movieService)
        {
            _context = context;
            _movieService = movieService;
        }

        // Add a new showtime
        public async Task<bool> AddShowtime(Showtime showtime)
        {
            // Check if the showtime already exists by ID
            if (await _context.Showtime.AnyAsync(s => s.Id == showtime.Id))
            {
                return false;  // Showtimes with the same ID already exist
            }

            // Set tickets available equal to capacity
            showtime.TicketsAvailable = showtime.Capacity;
            Console.WriteLine($"Showtime created with ID: {showtime.Id}, Capacity: {showtime.Capacity}, TicketsAvailable: {showtime.TicketsAvailable}");

            // Create tickets based on the showtime's capacity
            for (int i = 0; i < showtime.Capacity; i++)
            {
                showtime.Tickets.Add(new Ticket
                {
                    Price = 10.00m, // Default ticket price
                    Showtime = showtime
                });
            }

            // Add the new showtime to the database
            _context.Showtime.Add(showtime);
            await _context.SaveChangesAsync();

            // Update the associated movie's showtimes list
            var movie = await _movieService.GetMovieById(showtime.MovieId);
            if (movie != null)
            {
                movie.Showtimes.Add(showtime);
                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();
            }

            return true;  // Showtimes successfully added
        }

    public async Task<bool> RemoveShowtime(int id)
    {
        var showtime = await _context.Showtime.FindAsync(id);
        if (showtime == null)
        {
            return false; // Showtime not found
        }

        _context.Showtime.Remove(showtime);
        await _context.SaveChangesAsync();

        return true;
    }

        // Edit an existing showtime
        public async Task<bool> EditShowtime(int id, Showtime editedShowtime)
        {
            // Find the existing showtime by ID, including its tickets
            var existingShowtime = await _context.Showtime
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (existingShowtime == null)
            {
                return false;  // Showtime not found
            }

            // Update fields for the showtime
            existingShowtime.StartTime = editedShowtime.StartTime;
            existingShowtime.MovieId = editedShowtime.MovieId;

            // Check if the capacity has changed
            if (editedShowtime.Capacity != existingShowtime.Capacity)
            {
                // Ensure the new capacity is greater than or equal to tickets sold
                if (editedShowtime.Capacity < existingShowtime.TicketsAvailable)
                {
                    throw new InvalidOperationException("New capacity cannot be less than the number of tickets available.");
                }

            existingShowtime.Capacity = editedShowtime.Capacity;
            existingShowtime.TicketsAvailable = editedShowtime.Capacity - (existingShowtime.Capacity - existingShowtime.TicketsAvailable);

        }

            try
            {
                // Save changes to the showtime
                _context.Showtime.Update(existingShowtime);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the error if any
                Console.WriteLine($"Error editing showtime: {ex.Message}");
                return false;  // Return false if an error occurs
            }
        }

        // Get showtimes for a specific movie by movie ID
        public async Task<List<ShowtimeDto>> GetShowtimesByMovieId(int movieId)
        {
            // Fetch showtimes for the given movie ID, including related tickets and movie details
            var showtimes = await _context.Showtime
                .Include(s => s.Tickets)
                .Include(s => s.Movie)
                .Where(s => s.MovieId == movieId)
                .ToListAsync();

            // Convert showtimes to ShowtimeDto for response
            return showtimes.Select(s => new ShowtimeDto
            {
                Id = s.Id,
                StartTime = s.StartTime,
                Tickets = s.Tickets.Select(t => new TicketDto
                {
                    Id = t.Id,
                    Price = t.Price
                }).ToList(),
                AvailableTickets = s.Capacity - s.Tickets.Count,  // Available tickets
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
}
