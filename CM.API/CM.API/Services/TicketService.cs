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
        public async Task<bool> RemoveTicketsFromMovie(int movieId, int numberOfTickets)
        {
            var movieTickets = await _context.Ticket.Where(t => t.Showtime.MovieId == movieId).ToListAsync();

            if (movieTickets.Count < numberOfTickets)
            {
                return false; // Not enough tickets to remove
            }

            _context.Ticket.RemoveRange(movieTickets.Take(numberOfTickets));
            await _context.SaveChangesAsync();

            return true;
        }

    }
}