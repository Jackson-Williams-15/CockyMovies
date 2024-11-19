using CM.API.Models;
using System.Collections.Generic;

namespace CM.API.Interfaces
{
    public interface ITicketService
    {
        Task<bool> AddTicket(Ticket ticket);
        Task<List<Ticket>> GetAllTickets();
        Task<List<Ticket>> GetTicketsByMovieId(int movieId);
        Task<TicketDto> GetTicketById(int id);
        Task<bool> RemoveTicketsFromMovie(int movieId, int numberOfTickets);

    }
}
