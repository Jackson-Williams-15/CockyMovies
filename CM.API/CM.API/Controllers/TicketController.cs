using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public IActionResult AddTicket([FromBody] Ticket ticket)
        {
            if (ticket == null)
            {
                return BadRequest("Ticket cannot be null.");
            }

            if (_ticketService.AddTicket(ticket))
            {
                return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, ticket);
            }

            return BadRequest("Ticket already exists or invalid data.");
        }

        // GET: api/tickets
        [HttpGet]
        public IActionResult GetAllTickets()
        {
            var tickets = _ticketService.GetAllTickets();
            return Ok(tickets);
        }

        // GET: api/tickets/by-movie/{movieId}
        [HttpGet("by-movie/{movieId}")]
        public IActionResult GetTicketsByMovieId(int movieId)
        {
            var tickets = _ticketService.GetTicketsByMovieId(movieId);
            if (tickets == null || !tickets.Any())
            {
                return NotFound("No tickets found for this movie.");
            }

            return Ok(tickets);
        }

        // GET: api/tickets/{id}
        [HttpGet("{id}")]
        public IActionResult GetTicketById(int id)
        {
            var ticket = _ticketService.GetAllTickets().FirstOrDefault(t => t.Id == id);
            if (ticket == null)
            {
                return NotFound("Ticket not found.");
            }

            return Ok(ticket);
        }
    }
}
