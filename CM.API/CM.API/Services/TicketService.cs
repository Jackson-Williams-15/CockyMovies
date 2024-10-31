using CM.API.Interfaces;
using CM.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace CM.API.Services
{
    public class TicketService : ITicketService {
        private readonly List<Ticket> _tickets;
        private readonly IMovieService _movieService;
        private int _nextId;

        public TicketService(IMovieService movieService)
        {
            _tickets = new List<Ticket>();
            _movieService = movieService;
            _nextId = 1;
        }

        // Add a new ticket
        public bool AddTicket(Ticket ticket)
        {
            // Check if the ticket already exists
            if (_tickets.Any(t => t.Id == ticket.Id))
            {
                return false; 
            }

            ticket.Id = _nextId++; 
            _tickets.Add(ticket); 

            

            return true; // Return success
        }

        // Get all tickets
        public List<Ticket> GetAllTickets()
        {
            return _tickets; 
        }
 
        // Get tickets by showtime
        public List<Ticket> GetTicketsByMovieId(int movieId)
        {

            return _tickets.Where(t => t.Showtime.MovieId == movieId).ToList(); 
        }

    }
}
