using CM.API.Data;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShowtimesController : ControllerBase
{
    private readonly IShowtimeService _showtimeService;
    private readonly IMovieService _movieService;
    private readonly AppDbContext _context;
    private readonly ILogger<ShowtimesController> _logger;
    public ShowtimesController(IShowtimeService showtimeService, IMovieService movieService, AppDbContext context, ILogger<ShowtimesController> logger)
    {
        _showtimeService = showtimeService;
        _movieService = movieService;
        _context = context;
        _logger = logger;
        _context = context;
    }

    // POST: api/showtimes
    [HttpPost]
    public async Task<IActionResult> AddShowtime([FromBody] ShowtimeCreateDto showtimeDto)
    {
        var movie = await _movieService.GetMovieById(showtimeDto.MovieId);

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

        var success = await _showtimeService.AddShowtime(showtime);

        if (!success)
        {
            return BadRequest("A showtime with the same ID already exists.");
        }

        return Ok("Showtime added successfully.");
    }

    // GET: api/showtimes/movie/{movieId}
    [HttpGet("movie/{movieId}")]
    public async Task<IActionResult> GetShowtimesByMovieId(int movieId)
    {
        var showtimes = await _showtimeService.GetShowtimesByMovieId(movieId);

        if (showtimes == null || showtimes.Count == 0)
        {
            return NotFound("No showtimes found for the specified movie.");
        }

        return Ok(showtimes);
    }

    // GET: api/showtimes/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetShowtimeById(int id)
    {
        var showtime = await _context.Showtime
            .Include(s => s.Movie)
            .Include(s => s.Tickets)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (showtime == null)
        {
            _logger.LogWarning($"Showtime with ID {id} not found.");
            return NotFound("Showtime not found.");
        }

        var showtimeDto = new ShowtimeDto
        {
            Id = showtime.Id,
            StartTime = showtime.StartTime,
            Movie = new MovieDto
            {
                Id = showtime.Movie.Id,
                Title = showtime.Movie.Title,
                Description = showtime.Movie.Description,
                Rating = showtime.Movie.Rating?.ToString() ?? "Unrated",
                DateReleased = showtime.Movie.DateReleased
            },
            Tickets = showtime.Tickets.Select(t => new TicketDto
            {
                Id = t.Id,
                Price = t.Price
            }).ToList(),
            AvailableTickets = showtime.TicketsAvailable
        };

        return Ok(showtimeDto);
    }
}