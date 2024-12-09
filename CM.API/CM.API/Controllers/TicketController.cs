using CM.API.Interfaces; // Importing the ITicketService interface for business logic layer interaction
using CM.API.Models; // Importing the Ticket and other related models
using Microsoft.AspNetCore.Mvc; // Importing ASP.NET Core MVC for API controller features
using System.Collections.Generic; // Importing collections (like List<T>)
using System.Linq; // Importing LINQ for querying collections
using System.Threading.Tasks; // Importing async/await functionality for handling asynchronous operations

namespace CM.API.Controllers
{
    // Attribute specifying that this class is an API controller
    [ApiController]
    
    // Route definition for the controller. API endpoint will start with api/tickets
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        // Declare the service interface to interact with ticket business logic
        private readonly ITicketService _ticketService;

        // Constructor that initializes the ticket service
        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService; // Dependency injection for the ITicketService interface
        }

        // POST: api/tickets - Add a new ticket
        [HttpPost]
        public async Task<IActionResult> AddTicket([FromBody] Ticket ticket)
        {
            // Check if the ticket is null
            if (ticket == null)
            {
                return BadRequest("Ticket cannot be null."); // Return bad request if the ticket is null
            }

            // Call service to add ticket and check if it was successful
            if (await _ticketService.AddTicket(ticket))
            {
                // If ticket is successfully added, return the CreatedAtAction with the ticket ID
                return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, ticket);
            }

            // Return bad request if ticket already exists or if data is invalid
            return BadRequest("Ticket already exists or invalid data.");
        }

        // PUT: api/tickets/edit-price/{showtimeId} - Edit ticket price for a specific showtime
        [HttpPut("edit-price/{showtimeId}")]
        public async Task<IActionResult> EditTicketPrice(int showtimeId, [FromBody] EditTicketPriceDto editTicketPriceDto)
        {
            // Validate that the new price is greater than zero
            if (editTicketPriceDto.NewPrice <= 0)
            {
                return BadRequest("Invalid price."); // Return bad request if the price is invalid
            }

            // Call service to edit the ticket price for the given showtime
            var success = await _ticketService.EditTicket(showtimeId, editTicketPriceDto.NewPrice);

            if (success)
            {
                // If price update is successful, return OK with a success message
                return Ok("Ticket prices updated successfully.");
            }

            // If showtime not found or update failed, return NotFound
            return NotFound("Showtime not found or update failed.");
        }

        // GET: api/tickets - Retrieve all tickets
        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            // Call service to get all tickets
            var tickets = await _ticketService.GetAllTickets();

            // Return OK with the list of all tickets
            return Ok(tickets);
        }

        // GET: api/tickets/by-movie/{movieId} - Retrieve tickets for a specific movie
        [HttpGet("by-movie/{movieId}")]
        public async Task<IActionResult> GetTicketsByMovieId(int movieId)
        {
            // Call service to get tickets by movie ID
            var tickets = await _ticketService.GetTicketsByMovieId(movieId);

            // Check if tickets are found
            if (tickets == null || !tickets.Any())
            {
                // Return NotFound if no tickets are found for the movie
                return NotFound("No tickets found for this movie.");
            }

            // Return OK with the list of tickets for the movie
            return Ok(tickets);
        }

        // GET: api/tickets/{id} - Retrieve a ticket by its ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            // Call service to get a ticket by ID
            var ticket = await _ticketService.GetTicketById(id);

            // If the ticket is not found, return NotFound
            if (ticket == null)
            {
                return NotFound("Ticket not found.");
            }

            // Return OK with the ticket details
            return Ok(ticket);
        }

        // POST: api/tickets/add-to-showtime/{showtimeId}/{numberOfTickets} - Add tickets to a showtime
        [HttpPost("add-to-showtime/{showtimeId}/{numberOfTickets}")]
        public async Task<IActionResult> AddTicketsToShowtime(int showtimeId, int numberOfTickets)
        {
            // Call service to add tickets to the showtime
            if (await _ticketService.AddTicketsToShowtime(showtimeId, numberOfTickets))
            {
                // Return OK if tickets were added successfully
                return Ok("Tickets added successfully.");
            }

            // Return BadRequest if adding tickets failed
            return BadRequest("Failed to add tickets. Ensure the showtime exists and input is valid.");
        }

        // DELETE: api/tickets/remove-from-showtime/{showtimeId}/{numberOfTickets} - Remove tickets from a showtime
        [HttpDelete("remove-from-showtime/{showtimeId}/{numberOfTickets}")]
        public async Task<IActionResult> RemoveTicketsFromShowtime(int showtimeId, int numberOfTickets)
        {
            // Call service to remove tickets from the showtime
            if (await _ticketService.RemoveTicketsFromShowtime(showtimeId, numberOfTickets))
            {
                // Return OK if tickets were successfully removed
                return Ok("Tickets removed successfully.");
            }

            // Return BadRequest if removing tickets failed
            return BadRequest("Failed to remove tickets. Ensure the showtime exists and has enough tickets available.");
        }
    }
}
