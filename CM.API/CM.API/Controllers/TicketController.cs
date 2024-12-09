using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.API.Controllers
{
    // API controller for managing tickets
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService; // Ticket service dependency

        // Constructor that injects the ITicketService dependency
        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        /// <summary>
        /// Adds a new ticket.
        /// </summary>
        /// <param name="ticket">The ticket to be added.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of adding the ticket.</returns>
        // POST: api/tickets
        [HttpPost]
        public async Task<IActionResult> AddTicket([FromBody] Ticket ticket)
        {
            // Checks if the ticket is null
            if (ticket == null)
            {
                return BadRequest("Ticket cannot be null.");
            }

            // Attempts to add the ticket and returns success or failure response
            if (await _ticketService.AddTicket(ticket))
            {
                // Returns a Created response with the location of the newly created ticket
                return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, ticket);
            }

            return BadRequest("Ticket already exists or invalid data.");
        }

        /// <summary>
        /// Edits the price of an existing ticket.
        /// </summary>
        /// <param name="showtimeId">The showtime ID associated with the ticket.</param>
        /// <param name="editTicketPriceDto">The data transfer object containing the new ticket price.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of editing the ticket price.</returns>
        // PUT: api/tickets/edit-price/{showtimeId}
        [HttpPut("edit-price/{showtimeId}")]
        public async Task<IActionResult> EditTicketPrice(int showtimeId, [FromBody] EditTicketPriceDto editTicketPriceDto)
        {
            // Validates that the new price is positive
            if (editTicketPriceDto.NewPrice <= 0)
            {
                return BadRequest("Invalid price.");
            }

            // Attempts to update the ticket price and returns success or failure response
            var success = await _ticketService.EditTicket(showtimeId, editTicketPriceDto.NewPrice);

            if (success)
            {
                return Ok("Ticket prices updated successfully.");
            }

            return NotFound("Showtime not found or update failed.");
        }

        /// <summary>
        /// Retrieves all tickets.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the result of retrieving all tickets.</returns>
        // GET: api/tickets
        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTickets();
            return Ok(tickets);
        }

        /// <summary>
        /// Retrieves tickets associated with a specific movie.
        /// </summary>
        /// <param name="movieId">The movie ID to fetch tickets for.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of retrieving tickets by movie.</returns>
        // GET: api/tickets/by-movie/{movieId}
        [HttpGet("by-movie/{movieId}")]
        public async Task<IActionResult> GetTicketsByMovieId(int movieId)
        {
            var tickets = await _ticketService.GetTicketsByMovieId(movieId);
            
            // Checks if tickets are found for the movie
            if (tickets == null || !tickets.Any())
            {
                return NotFound("No tickets found for this movie.");
            }

            return Ok(tickets);
        }

        /// <summary>
        /// Retrieves a specific ticket by its ID.
        /// </summary>
        /// <param name="id">The ticket ID.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of retrieving the ticket by ID.</returns>
        // GET: api/tickets/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            var ticket = await _ticketService.GetTicketById(id);

            // Returns a NotFound response if the ticket does not exist
            if (ticket == null)
            {
                return NotFound("Ticket not found.");
            }

            return Ok(ticket);
        }

        /// <summary>
        /// Adds a specified number of tickets to a showtime.
        /// </summary>
        /// <param name="showtimeId">The showtime ID to add tickets to.</param>
        /// <param name="numberOfTickets">The number of tickets to add.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of adding tickets to the showtime.</returns>
        // POST: api/tickets/add-to-showtime/{showtimeId}/{numberOfTickets}
        [HttpPost("add-to-showtime/{showtimeId}/{numberOfTickets}")]
        public async Task<IActionResult> AddTicketsToShowtime(int showtimeId, int numberOfTickets)
        {
            // Attempts to add tickets to the showtime and returns success or failure response
            if (await _ticketService.AddTicketsToShowtime(showtimeId, numberOfTickets))
            {
                return Ok("Tickets added successfully.");
            }

            return BadRequest("Failed to add tickets. Ensure the showtime exists and input is valid.");
        }

        /// <summary>
        /// Removes a specified number of tickets from a showtime.
        /// </summary>
        /// <param name="showtimeId">The showtime ID to remove tickets from.</param>
        /// <param name="numberOfTickets">The number of tickets to remove.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of removing tickets from the showtime.</returns>
        // DELETE: api/tickets/remove-from-showtime/{showtimeId}/{numberOfTickets}
        [HttpDelete("remove-from-showtime/{showtimeId}/{numberOfTickets}")]
        public async Task<IActionResult> RemoveTicketsFromShowtime(int showtimeId, int numberOfTickets)
        {
            // Attempts to remove tickets from the showtime and returns success or failure response
            if (await _ticketService.RemoveTicketsFromShowtime(showtimeId, numberOfTickets))
            {
                return Ok("Tickets removed successfully.");
            }

            return BadRequest("Failed to remove tickets. Ensure the showtime exists and has enough tickets available.");
        }
    }
}
