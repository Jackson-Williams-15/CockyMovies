using CM.API.Models;
using System.Collections.Generic;

namespace CM.API.Interfaces
{
    /// <summary>
    /// Interface for ticket service operations.
    /// </summary>
    /// <seealso cref="CM.API.Services.TicketService"/>
    public interface ITicketService
    {
        /// <summary>
        /// Adds a new ticket.
        /// </summary>
        /// <param name="ticket">The ticket to add.</param>
        /// <returns>
        /// <seealso cref="CM.API.Services.TicketService.AddTicket(Ticket)"/>
        Task<bool> AddTicket(Ticket ticket);

        /// <summary>
        /// Gets all tickets.
        /// </summary>
        /// <seealso cref="CM.API.Services.TicketService.GetAllTickets"/>
        Task<List<Ticket>> GetAllTickets();

        /// <summary>
        /// Gets tickets by movie ID.
        /// </summary>
        /// <param name="movieId">The movie ID.</param>
        /// <seealso cref="CM.API.Services.TicketService.GetTicketsByMovieId(int)"/>
        Task<List<Ticket>> GetTicketsByMovieId(int movieId);

        /// <summary>
        /// Edits the price of tickets for a specific showtime.
        /// </summary>
        /// <param name="showtimeId">The showtime ID.</param>
        /// <param name="newPrice">The new ticket price.</param>
        /// <seealso cref="CM.API.Services.TicketService.EditTicket(int, decimal)"/>
        Task<bool> EditTicket(int showtimeId, decimal newPrice);

        /// <summary>
        /// Gets a ticket by ID.
        /// </summary>
        /// <param name="id">The ticket ID.</param>
        /// <seealso cref="CM.API.Services.TicketService.GetTicketById(int)"/>
        Task<TicketDto?> GetTicketById(int id);

        /// <summary>
        /// Removes tickets from a specific showtime.
        /// </summary>
        /// <param name="showtimeId">The showtime ID.</param>
        /// <param name="numberOfTickets">The number of tickets to remove.</param>
        /// <seealso cref="CM.API.Services.TicketService.RemoveTicketsFromShowtime(int, int)"/>
        Task<bool> RemoveTicketsFromShowtime(int movieId, int numberOfTickets);

        /// <summary>
        /// Adds tickets to a specific showtime.
        /// </summary>
        /// <param name="showtimeId">The showtime ID.</param>
        /// <param name="numberOfTickets">The number of tickets to add.</param>
        /// <seealso cref="CM.API.Services.TicketService.AddTicketsToShowtime(int, int)"/>
        Task<bool> AddTicketsToShowtime(int showtimeId, int numberOfTickets);

    }
}
