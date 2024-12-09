using CM.API.Models;
using System.Collections.Generic;

namespace CM.API.Interfaces
{
    public interface ITicketService
    {
        Task<bool> AddTicket(Ticket ticket);
        Task<List<Ticket>> GetAllTickets();
        Task<List<Ticket>> GetTicketsByMovieId(int movieId);
        Task<bool> EditTicket(int showtimeId, decimal newPrice);
        Task<TicketDto> GetTicketById(int id);
        Task<bool> RemoveTicketsFromShowtime(int movieId, int numberOfTickets);
        Task<bool> AddTicketsToShowtime(int showtimeId, int numberOfTickets);

    }
}
