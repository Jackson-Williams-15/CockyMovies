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
    /// Service for managing tickets.
    /// </summary>
    public class TicketService : ITicketService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketService"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public TicketService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new ticket.
        /// </summary>
        /// <param name="ticket">The ticket to add.</param>
        /// <returns>
        /// true if the ticket was added successfully, false otherwise.
        /// </returns>
        public async Task<bool> AddTicket(Ticket ticket)
        {
            // Check if the ticket already exists
            if (await _context.Ticket.AnyAsync(t => t.Id == ticket.Id))
            {
                return false;
            }

            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Edits the price of tickets for a specific showtime.
        /// </summary>
        /// <param name="showtimeId">The showtime ID.</param>
        /// <param name="newPrice">The new ticket price.</param>
        /// <returns>
        /// true if the ticket prices were updated successfully, false otherwise.
        /// </returns>
        public async Task<bool> EditTicket(int showtimeId, decimal newPrice)
        {
            // Find the showtime
            var showtime = await _context.Showtime
                .Include(s => s.Tickets) // Include tickets to make sure we can access them
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

            // Save changes to the database
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Gets all tickets.
        /// </summary>
        /// <returns>
        /// Returns a list of all tickets.
        /// </returns>
        public async Task<List<Ticket>> GetAllTickets()
        {
            return await _context.Ticket.ToListAsync();
        }

        /// <summary>
        /// Gets tickets by movie ID.
        /// </summary>
        /// <param name="movieId">The movie ID.</param>
        /// <returns>
        /// A list of tickets for the specified movie showtime.
        /// </returns>
        public async Task<List<Ticket>> GetTicketsByMovieId(int movieId)
        {
            return await _context.Ticket
                                 .Where(t => t.Showtime.MovieId == movieId)
                                 .ToListAsync();
        }

        /// <summary>
        /// Gets a ticket by ID.
        /// </summary>
        /// <param name="id">The ticket ID.</param>
        /// <returns>
        /// The ticket details, or null if the ticket is not found.
        /// </returns>
        public async Task<TicketDto?> GetTicketById(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null) return null;

            return new TicketDto
            {
                Id = ticket.Id,
                Price = ticket.Price
            };
        }

        /// <summary>
        /// Removes tickets from a specific showtime.
        /// </summary>
        /// <param name="showtimeId">The showtime ID.</param>
        /// <param name="numberOfTickets">The number of tickets to remove.</param>
        /// <returns>
        /// A value of true if the tickets were removed successfully, false otherwise.
        /// </returns>
        public async Task<bool> RemoveTicketsFromShowtime(int showtimeId, int numberOfTickets)
        {
            if (numberOfTickets <= 0)
            {
                return false; // Invalid number of tickets
            }

            // Find the showtime
            var showtime = await _context.Showtime
                .Include(s => s.Tickets) // Include tickets to make sure we can access them
                .FirstOrDefaultAsync(s => s.Id == showtimeId);

            if (showtime == null)
            {
                return false; // Showtime not found
            }

            if (showtime.TicketsAvailable == showtime.Capacity)
            {
                return false;
            }

            // Get the tickets associated with this showtime
            var ticketsToRemove = showtime.Tickets
                .Take(numberOfTickets) // Limit to the number of tickets you want to remove
                .ToList();

            // Check if there are enough tickets
            if (ticketsToRemove.Count < numberOfTickets)
            {
                return false; // Not enough tickets to remove
            }

            // Remove the tickets from the Ticket table
            _context.Ticket.RemoveRange(ticketsToRemove);

            // Update the Showtime's available ticket count (if applicable)
            showtime.TicketsAvailable -= numberOfTickets; // Decrease ticket count

            // Save changes to the database
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Adds tickets to a specific showtime.
        /// </summary>
        /// <param name="showtimeId">The showtime ID.</param>
        /// <param name="numberOfTickets">The number of tickets to add.</param>
        /// <returns>
        /// A value of true if the tickets were added successfully, false otherwise.
        /// </returns>
        public async Task<bool> AddTicketsToShowtime(int showtimeId, int numberOfTickets)
        {
            if (numberOfTickets <= 0)
            {
                return false; // Invalid number of tickets
            }

            // Find the showtime
            var showtime = await _context.Showtime
                .Include(s => s.Tickets) // Include tickets to make sure we can access them
                .FirstOrDefaultAsync(s => s.Id == showtimeId);

            if (showtime == null)
            {
                return false; // Showtime not found
            }
            //if adding the tickets would exceed the capacity
            if (showtime.TicketsAvailable + numberOfTickets > showtime.Capacity)
            {
                return false;
            }

            // Create new tickets
            var newTickets = new List<Ticket>();
            for (int i = 0; i < numberOfTickets; i++)
            {
                newTickets.Add(new Ticket
                {
                    Showtime = showtime,
                    Price = 10.00m // Assuming movie has a TicketPrice property
                });
            }

            // Add the new tickets to the Ticket table
            _context.Ticket.AddRange(newTickets);

            // Update the Showtime's available ticket count (if applicable)
            showtime.TicketsAvailable += numberOfTickets; // Increase ticket count

            // Save changes to the database
            await _context.SaveChangesAsync();

            return true;
        }
    }
}