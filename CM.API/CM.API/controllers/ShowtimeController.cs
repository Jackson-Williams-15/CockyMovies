using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CM.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ShowtimesController : ControllerBase
{
    private readonly IShowtimeService _showtimeService;
    private readonly IMovieService _movieService;

    public ShowtimesController(IShowtimeService showtimeService, IMovieService movieService)
    {
        _showtimeService = showtimeService;
        _movieService = movieService;
    }

    // POST: api/showtimes
    [HttpPost]
    public IActionResult AddShowtime([FromBody] ShowtimeCreateDto showtimeDto)
    {
        var movie = _movieService.GetMovieById(showtimeDto.MovieId);

        if (movie == null)
        {
            return NotFound("Movie not found.");
        }

        var showtime = new Showtime
        {
            StartTime = showtimeDto.StartTime,
            MovieId = showtimeDto.MovieId,
            Movie = movie,
            Capacity = showtimeDto.Capacity,
            Tickets = new List<Ticket>(),
        };

        var success = _showtimeService.AddShowtime(showtime);

        if (!success)
        {
            return BadRequest("A showtime with the same ID already exists.");
        }

        return Ok("Showtime added successfully.");
    }

    // GET: api/showtimes/movie/{movieId}
    [HttpGet("movie/{movieId}")]
    public IActionResult GetShowtimesByMovieId(int movieId)
    {
        var showtimes = _showtimeService.GetShowtimesByMovieId(movieId);

        if (!showtimes.Any())
        {
            return NotFound("No showtimes found for this movie.");
        }

        var showtimeDtos = showtimes.Select(s => new ShowtimeDto
        {
            Id = s.Id,
            StartTime = s.StartTime,
            Tickets = s.Tickets.Select(t => new TicketDto
            {
                Id = t.Id,
                Price = t.Price
            }).ToList()
        }).ToList();

        return Ok(showtimeDtos);
    }
}