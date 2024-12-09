using CM.API.Data;  // Database context
using CM.API.Interfaces;  // Interfaces for services
using CM.API.Models;  // Models like Ticket, Showtime, etc.
using Microsoft.EntityFrameworkCore;  // Entity Framework Core for data access
using System.Collections.Generic;  // For working with lists
using System.Linq;  // LINQ queries
using System.Threading.Tasks;  // Asynchronous programming

namespace CM.API.Services
{
    public class TicketService : ITicketService
    {
        private readonly AppDbContext _context;  // Database context

        // Constructor to inject the AppDbContext
        public TicketService(AppDbContext context)
        {
            _context = context;
        }

        // Adds a new ticket to the database
        public async Task<bool> AddTicket(Ticket ticket)
        {
            // Check if the ticket already exists by its ID
            if (await _context.Ticket.AnyAsync(t => t.Id == ticket.Id))
            {
                return false; // Ticket already exists
            }

            _context.Ticket.Add(ticket);  // Add the new ticket
            await _context.SaveChangesAsync();  // Save changes to the database
            return true;
        }

        // Edits the price of all tickets for a given showtime
        public async Task<bool> EditTicket(int showtimeId, decimal newPrice)
        {
            // Find the showtime and include tickets
            var showtime = await _context.Showtime
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(s => s.Id == showtimeId);

            if (showtime == null)
            {
                return false; // Showtime not found
            }

            // Update the price of all tickets for the showtime
            foreach (var ticket in showtime.Tickets)
            {
                ticket.Price = newPrice;
            }

            await _context.SaveChangesAsync();  // Save changes
            return true;
        }

        // Gets all tickets from the database
        public async Task<List<Ticket>> GetAllTickets()
        {
            return await _context.Ticket.ToListAsync();  // Fetch all tickets
        }

        // Gets tickets for a specific movie
        public async Task<List<Ticket>> GetTicketsByMovieId(int movieId)
        {
            // Filter tickets by movieId
            return await _context.Ticket
                                 .Where(t => t.Showtime.MovieId == movieId)
                                 .ToListAsync();
        }

        // Gets a specific ticket by ID and returns a DTO
        public async Task<TicketDto> GetTicketById(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null) return null;  // Ticket not found

            return new TicketDto
            {
                Id = ticket.Id,
                Price = ticket.Price
            };
        }

        // Removes tickets from a showtime
        public async Task<bool> RemoveTicketsFromShowtime(int showtimeId, int numberOfTickets)
        {
            if (numberOfTickets <= 0)
            {
                return false; // Invalid number of tickets
            }

            // Find the showtime and include its tickets
            var showtime = await _context.Showtime
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(s => s.Id == showtimeId);

            if (showtime == null)
            {
                return false; // Showtime not found
            }

            if (showtime.TicketsAvailable == showtime.Capacity)
            {
                return false; // No tickets to remove
            }

            // Get the tickets to remove
            var ticketsToRemove = showtime.Tickets
                .Take(numberOfTickets)  // Limit the removal to the specified number
                .ToList();

            // Ensure there are enough tickets to remove
            if (ticketsToRemove.Count < numberOfTickets)
            {
                return false; // Not enough tickets to remove
            }

            _context.Ticket.RemoveRange(ticketsToRemove);  // Remove the tickets from the database
            showtime.TicketsAvailable -= numberOfTickets;  // Update the available tickets count

            await _context.SaveChangesAsync();  // Save changes
            return true;
        }

        // Adds tickets to a showtime
        public async Task<bool> AddTicketsToShowtime(int showtimeId, int numberOfTickets)
        {
            if (numberOfTickets <= 0)
            {
                return false; // Invalid number of tickets
            }

            // Find the showtime and include its tickets
            var showtime = await _context.Showtime
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(s => s.Id == showtimeId);

            if (showtime == null)
            {
                return false; // Showtime not found
            }

            // Ensure the tickets won't exceed the showtime's capacity
            if (showtime.TicketsAvailable + numberOfTickets > showtime.Capacity)
            {
                return false; // Exceeds capacity
            }

            // Create new tickets
            var newTickets = new List<Ticket>();
            for (int i = 0; i < numberOfTickets; i++)
            {
                newTickets.Add(new Ticket
                {
                    Showtime = showtime,
                    Price = 10.00m  // Default price (could be dynamic if needed)
                });
            }

            _context.Ticket.AddRange(newTickets);  // Add the new tickets to the database
            showtime.TicketsAvailable += numberOfTickets;  // Update the available ticket count

            await _context.SaveChangesAsync();  // Save changes
            return true;
        }
    }
}
