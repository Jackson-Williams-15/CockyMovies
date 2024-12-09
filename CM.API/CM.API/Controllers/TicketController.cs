using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.API.Controllers
{
    /// <summary>
    /// Controller for managing tickets.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketsController"/> class.
        /// </summary>
        /// <param name="ticketService">The ticket service.</param>
        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        /// <summary>
        /// Adds a new ticket.
        /// </summary>
        /// <param name="ticket">The ticket to add.</param>
        /// <returns>
        /// A <see cref="CreatedAtActionResult"/> if the ticket is successfully added,
        /// or a <see cref="BadRequestObjectResult"/> if the ticket is null or already exists.
        /// </returns>
        // POST: api/tickets
        [HttpPost]
        public async Task<IActionResult> AddTicket([FromBody] Ticket ticket)
        {
            if (ticket == null)
            {
                return BadRequest("Ticket cannot be null.");
            }

            if (await _ticketService.AddTicket(ticket))
            {
                return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, ticket);
            }

            return BadRequest("Ticket already exists or invalid data.");
        }

        /// <summary>
        /// Edits the price of tickets for a specific showtime.
        /// </summary>
        /// <param name="showtimeId">The showtime ID.</param>
        /// <param name="editTicketPriceDto">The new ticket price details.</param>
        /// <returns>
        /// An <see cref="OkObjectResult"/> if the ticket prices are successfully updated,
        /// or a <see cref="BadRequestObjectResult"/> if the price is invalid,
        /// or a <see cref="NotFoundObjectResult"/> if the showtime is not found or the update fails.
        /// </returns>
        // PUT: api/tickets/edit-price/{showtimeId}
        [HttpPut("edit-price/{showtimeId}")]
        public async Task<IActionResult> EditTicketPrice(int showtimeId, [FromBody] EditTicketPriceDto editTicketPriceDto)
        {
            if (editTicketPriceDto.NewPrice <= 0)
            {
                return BadRequest("Invalid price.");
            }

            var success = await _ticketService.EditTicket(showtimeId, editTicketPriceDto.NewPrice);

            if (success)
            {
                return Ok("Ticket prices updated successfully.");
            }

            return NotFound("Showtime not found or update failed.");
        }

        /// <summary>
        /// Gets all tickets.
        /// </summary>
        /// <returns>
        /// An <see cref="OkObjectResult"/> containing the list of all tickets.
        /// </returns>
        // GET: api/tickets
        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTickets();
            return Ok(tickets);
        }

        /// <summary>
        /// Gets tickets by movie ID.
        /// </summary>
        /// <param name="movieId">The movie ID.</param>
        /// <returns>
        /// An <see cref="OkObjectResult"/> containing the list of tickets for the specified movie,
        /// or a <see cref="NotFoundObjectResult"/> if no tickets are found for the movie.
        /// </returns>
        // GET: api/tickets/by-movie/{movieId}
        [HttpGet("by-movie/{movieId}")]
        public async Task<IActionResult> GetTicketsByMovieId(int movieId)
        {
            var tickets = await _ticketService.GetTicketsByMovieId(movieId);
            if (tickets == null || !tickets.Any())
            {
                return NotFound("No tickets found for this movie.");
            }

            return Ok(tickets);
        }

        /// <summary>
        /// Gets a ticket by ID.
        /// </summary>
        /// <param name="id">The ticket ID.</param>
        /// <returns>
        /// An <see cref="OkObjectResult"/> containing the ticket details,
        /// or a <see cref="NotFoundObjectResult"/> if the ticket is not found.
        /// </returns>
        // GET: api/tickets/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            var ticket = await _ticketService.GetTicketById(id);
            if (ticket == null)
            {
                return NotFound("Ticket not found.");
            }

            return Ok(ticket);
        }

        /// <summary>
        /// Adds tickets to a specific showtime.
        /// </summary>
        /// <param name="showtimeId">The showtime ID.</param>
        /// <param name="numberOfTickets">The number of tickets to add.</param>
        /// <returns>
        /// An <see cref="OkObjectResult"/> if the tickets are successfully added,
        /// or a <see cref="BadRequestObjectResult"/> if the addition fails.
        /// </returns>
        // POST: api/tickets/add-to-showtime/{showtimeId}/{numberOfTickets}
        [HttpPost("add-to-showtime/{showtimeId}/{numberOfTickets}")]
        public async Task<IActionResult> AddTicketsToShowtime(int showtimeId, int numberOfTickets)
        {
            if (await _ticketService.AddTicketsToShowtime(showtimeId, numberOfTickets))
            {
                return Ok("Tickets added successfully.");
            }

            return BadRequest("Failed to add tickets. Ensure the showtime exists and input is valid.");
        }

        /// <summary>
        /// Removes tickets from a specific showtime.
        /// </summary>
        /// <param name="showtimeId">The showtime ID.</param>
        /// <param name="numberOfTickets">The number of tickets to remove.</param>
        /// <returns>
        /// An <see cref="OkObjectResult"/> if the tickets are successfully removed,
        /// or a <see cref="BadRequestObjectResult"/> if the removal fails.
        /// </returns>
        // DELETE: api/tickets/remove-from-showtime/{showtimeId}/{numberOfTickets}
        [HttpDelete("remove-from-showtime/{showtimeId}/{numberOfTickets}")]
        public async Task<IActionResult> RemoveTicketsFromShowtime(int showtimeId, int numberOfTickets)
        {
            if (await _ticketService.RemoveTicketsFromShowtime(showtimeId, numberOfTickets))
            {
                return Ok("Tickets removed successfully.");
            }

            return BadRequest("Failed to remove tickets. Ensure the showtime exists and has enough tickets available.");
        }



    }
}