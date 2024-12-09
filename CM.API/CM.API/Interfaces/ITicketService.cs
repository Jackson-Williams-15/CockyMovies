using CM.API.Models;
using System.Collections.Generic;

namespace CM.API.Interfaces
{
    /// <summary>
    /// Interface for the ticket service that defines the operations related to managing tickets.
    /// </summary>
    public interface ITicketService
    {
        /// <summary>
        /// Adds a new ticket.
        /// </summary>
        /// <param name="ticket">The ticket to be added.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the ticket was added successfully; otherwise, false.</returns>
        Task<bool> AddTicket(Ticket ticket);

        /// <summary>
        /// Retrieves all tickets.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of all tickets.</returns>
        Task<List<Ticket>> GetAllTickets();

        /// <summary>
        /// Retrieves tickets for a specific movie by its ID.
        /// </summary>
        /// <param name="movieId">The movie ID to filter tickets by.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of tickets for the specified movie.</returns>
        Task<List<Ticket>> GetTicketsByMovieId(int movieId);

        /// <summary>
        /// Edits the price of a ticket for a specific showtime.
        /// </summary>
        /// <param name="showtimeId">The showtime ID to edit the ticket price for.</param>
        /// <param name="newPrice">The new price of the ticket.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the ticket price was successfully updated; otherwise, false.</returns>
        Task<bool> EditTicket(int showtimeId, decimal newPrice);

        /// <summary>
        /// Retrieves a ticket by its ID.
        /// </summary>
        /// <param name="id">The ticket ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the ticket with the specified ID, or null if not found.</returns>
        Task<TicketDto?> GetTicketById(int id);

        /// <summary>
        /// Removes a specified number of tickets from a showtime.
        /// </summary>
        /// <param name="movieId">The movie ID associated with the showtime to remove tickets from.</param>
        /// <param name="numberOfTickets">The number of tickets to remove.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the tickets were successfully removed; otherwise, false.</returns>
        Task<bool> RemoveTicketsFromShowtime(int movieId, int numberOfTickets);

        /// <summary>
        /// Adds a specified number of tickets to a showtime.
        /// </summary>
        /// <param name="showtimeId">The showtime ID to add tickets to.</param>
        /// <param name="numberOfTickets">The number of tickets to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the tickets were successfully added; otherwise, false.</returns>
        Task<bool> AddTicketsToShowtime(int showtimeId, int numberOfTickets);
    }
}
