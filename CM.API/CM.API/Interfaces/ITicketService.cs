using CM.API.Models;
using System.Collections.Generic;

namespace CM.API.Interfaces
{
    public interface ITicketService
    {
        bool AddTicket(Ticket ticket);
        List<Ticket> GetAllTickets();
        List<Ticket> GetTicketsByMovieId(int movieId); // Ensure this method is defined here
    }
}
