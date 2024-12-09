using CM.API.Models;
using System.Collections.Generic;

namespace CM.API.Interfaces
{
    public interface ITicketService
    {
        // Add a new ticket
        Task<bool> AddTicket(Ticket ticket);

        // Get all tickets from the database
        Task<List<Ticket>> GetAllTickets();

        // Get all tickets associated with a specific movie (via movie ID)
        Task<List<Ticket>> GetTicketsByMovieId(int movieId);

        // Edit the price of tickets associated with a specific showtime
        Task<bool> EditTicket(int showtimeId, decimal newPrice);

        // Get a ticket by its ID and return as a DTO
        Task<TicketDto> GetTicketById(int id);

        // Remove a specified number of tickets from a given showtime (movieId likely is a mistake here)
        Task<bool> RemoveTicketsFromShowtime(int showtimeId, int numberOfTickets);

        // Add a specified number of tickets to a given showtime
        Task<bool> AddTicketsToShowtime(int showtimeId, int numberOfTickets);
    }
}
