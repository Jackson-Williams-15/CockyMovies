using CM.API.Data;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.API.Services
{
    /// <summary>
    /// Service implementation for managing tickets.
    /// </summary>
    public class TicketService : ITicketService
    {
        private readonly AppDbContext _context; // Database context

        /// <summary>
        /// Initializes a new instance of the TicketService class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public TicketService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new ticket to the database.
        /// </summary>
        /// <param name="ticket">The ticket to be added.</param>
        /// <returns>True if the ticket is added successfully, otherwise false.</returns>
        public async Task<bool> AddTicket(Ticket ticket)
        {
            // Check if the ticket already exists by its ID
            if (await _context.Ticket.AnyAsync(t => t.Id == ticket.Id))
            {
                return false; // Ticket already exists
            }

            _context.Ticket.Add(ticket); // Add the new ticket
            await _context.SaveChangesAsync(); // Save changes to the database
            return true;
        }

        /// <summary>
        /// Edits the price of tickets for a specific showtime.
        /// </summary>
        /// <param name="showtimeId">The ID of the showtime for which tickets' prices should be updated.</param>
        /// <param name="newPrice">The new price to be set for all tickets in the showtime.</param>
        /// <returns>True if the ticket prices are updated successfully, otherwise false.</returns>
        public async Task<bool> EditTicket(int showtimeId, decimal newPrice)
        {
            // Find the showtime, including its tickets
            var showtime = await _context.Showtime
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(s => s.Id == showtimeId);

            if (showtime == null)
            {
                return false; // Showtime not found
            }

            // Update the price of all tickets within the showtime
            foreach (var ticket in showtime.Tickets)
            {
                ticket.Price = newPrice;
            }

            await _context.SaveChangesAsync(); // Save changes to the database
            return true;
        }

        /// <summary>
        /// Retrieves all tickets from the database.
        /// </summary>
        /// <returns>A list of all tickets.</returns>
        public async Task<List<Ticket>> GetAllTickets()
        {
            return await _context.Ticket.ToListAsync(); // Retrieve all tickets
        }

        /// <summary>
        /// Retrieves tickets by movie ID.
        /// </summary>
        /// <param name="movieId">The movie ID to filter tickets by.</param>
        /// <returns>A list of tickets associated with the specified movie.</returns>
        public async Task<List<Ticket>> GetTicketsByMovieId(int movieId)
        {
            return await _context.Ticket
                                 .Where(t => t.Showtime.MovieId == movieId) // Filter by movie ID
                                 .ToListAsync();
        }

        /// <summary>
        /// Retrieves a ticket by its ID.
        /// </summary>
        /// <param name="id">The ticket ID.</param>
        /// <returns>A TicketDto representing the ticket, or null if not found.</returns>
        public async Task<TicketDto?> GetTicketById(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id); // Find the ticket by ID
            if (ticket == null) return null;

            return new TicketDto
            {
                Id = ticket.Id,
                Price = ticket.Price
            };
        }

        /// <summary>
        /// Removes a specified number of tickets from a showtime.
        /// </summary>
        /// <param name="showtimeId">The ID of the showtime from which tickets should be removed.</param>
        /// <param name="numberOfTickets">The number of tickets to remove.</param>
        /// <returns>True if tickets were removed successfully, otherwise false.</returns>
        public async Task<bool> RemoveTicketsFromShowtime(int showtimeId, int numberOfTickets)
        {
            if (numberOfTickets <= 0)
            {
                return false; // Invalid number of tickets
            }

            // Find the showtime, including its tickets
            var showtime = await _context.Showtime
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(s => s.Id == showtimeId);

            if (showtime == null || showtime.TicketsAvailable == showtime.Capacity)
            {
                return false; // Showtime not found or no tickets to remove
            }

            // Get the tickets associated with this showtime
            var ticketsToRemove = showtime.Tickets
                .Take(numberOfTickets)
                .ToList();

            // Check if there are enough tickets to remove
            if (ticketsToRemove.Count < numberOfTickets)
            {
                return false; // Not enough tickets to remove
            }

            _context.Ticket.RemoveRange(ticketsToRemove); // Remove the tickets from the database
            showtime.TicketsAvailable -= numberOfTickets; // Decrease the available ticket count
            await _context.SaveChangesAsync(); // Save changes to the database

            return true;
        }

        /// <summary>
        /// Adds a specified number of tickets to a showtime.
        /// </summary>
        /// <param name="showtimeId">The ID of the showtime to add tickets to.</param>
        /// <param name="numberOfTickets">The number of tickets to add.</param>
        /// <returns>True if tickets were added successfully, otherwise false.</returns>
        public async Task<bool> AddTicketsToShowtime(int showtimeId, int numberOfTickets)
        {
            if (numberOfTickets <= 0)
            {
                return false; // Invalid number of tickets
            }

            // Find the showtime, including its tickets
            var showtime = await _context.Showtime
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(s => s.Id == showtimeId);

            if (showtime == null)
            {
                return false; // Showtime not found
            }

            // Check if adding tickets would exceed the capacity
            if (showtime.TicketsAvailable + numberOfTickets > showtime.Capacity)
            {
                return false;
            }

            // Create new tickets and add them to the showtime
            var newTickets = new List<Ticket>();
            for (int i = 0; i < numberOfTickets; i++)
            {
                newTickets.Add(new Ticket
                {
                    Showtime = showtime,
                    Price = 10.00m // Assuming each ticket has a fixed price
                });
            }

            _context.Ticket.AddRange(newTickets); // Add the new tickets to the database
            showtime.TicketsAvailable += numberOfTickets; // Increase the available ticket count
            await _context.SaveChangesAsync(); // Save changes to the database

            return true;
        }
    }
}
