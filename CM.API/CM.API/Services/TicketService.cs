using CM.API.Data;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.API.Services
{
    public class TicketService : ITicketService
    {
        private readonly AppDbContext _context;

        public TicketService(AppDbContext context)
        {
            _context = context;
        }

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

        // Get all tickets
        public async Task<List<Ticket>> GetAllTickets()
        {
            return await _context.Ticket.ToListAsync();
        }

        // Get tickets by showtime
        public async Task<List<Ticket>> GetTicketsByMovieId(int movieId)
        {
            return await _context.Ticket
                                 .Where(t => t.Showtime.MovieId == movieId)
                                 .ToListAsync();
        }

        public async Task<TicketDto> GetTicketById(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null) return null;

            return new TicketDto
            {
                Id = ticket.Id,
                Price = ticket.Price
            };
        }

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

            if(showtime.TicketsAvailable == showtime.Capacity) {
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
            if(showtime.TicketsAvailable == showtime.Capacity) {
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