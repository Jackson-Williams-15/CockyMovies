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
        // DELETE: api/tickets/remove-from-movie/{movieId}/{numberOfTickets}
        [HttpDelete("remove-from-movie/{movieId}/{numberOfTickets}")]
        public async Task<IActionResult> RemoveTicketsFromMovie(int movieId, int numberOfTickets)
        {
            if (numberOfTickets <= 0)
            {
                return BadRequest("Number of tickets to remove must be greater than zero.");
            }

            var result = await _ticketService.RemoveTicketsFromMovie(movieId, numberOfTickets);

            if (result)
            {
                return Ok("Tickets removed successfully.");
            }

            return BadRequest("Failed to remove tickets. Ensure the movie exists and has enough tickets available.");
        }

    }
}