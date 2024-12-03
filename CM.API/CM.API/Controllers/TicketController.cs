using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

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

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTicket(int id, [FromBody] Ticket updatedTicket)
        {
            if (updatedTicket == null || id != updatedTicket.Id)
            {
                return BadRequest("Invalid ticket data.");
            }

            var result = await _ticketService.EditTicket(id, updatedTicket);
            if (!result)
            {
                return NotFound("Ticket not found.");
            }

            return NoContent();
        }

        // GET: api/tickets
        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTickets();
            return Ok(tickets);
        }

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