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

        if (showtimes.Count == 0)
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
        else
        {
            _logger.LogInformation($"Showtime with ID {id} found with start time {showtime.StartTime}");
        }

        if (showtime.Movie == null)
        {
            _logger.LogWarning($"Showtime with ID {id} has no associated movie.");
            return BadRequest("Showtime does not have an associated movie.");
        }
        else
        {
            _logger.LogInformation($"Associated movie with ID {showtime.Movie.Id} and title '{showtime.Movie.Title}' found.");
        }

        if (showtime.Tickets == null)
        {
            _logger.LogWarning($"Showtime with ID {id} has a null Tickets collection.");
        }
        else
        {
            _logger.LogInformation($"Showtime with ID {id} has {showtime.Tickets.Count} tickets.");
        }

        try
        {
            _logger.LogInformation("Creating ShowtimeDto...");

            // Log details of the movie object
            _logger.LogInformation($"Movie details: ID = {showtime.Movie.Id}, Title = {showtime.Movie.Title}, Description = {showtime.Movie.Description}, Rating = {showtime.Movie.Rating}, DateReleased = {showtime.Movie.DateReleased}");

            var movieDto = new MovieDto
            {
                Id = showtime.Movie.Id,
                Title = showtime.Movie.Title,
                Description = showtime.Movie.Description,
                Rating = showtime.Movie.Rating?.ToString() ?? "Unrated",
                DateReleased = showtime.Movie.DateReleased
            };
            _logger.LogInformation($"MovieDto created with ID {movieDto.Id}, Title {movieDto.Title}");

            var ticketDtos = showtime.Tickets?.Select(t => new TicketDto
            {
                Id = t.Id,
                Price = t.Price
            }).ToList() ?? new List<TicketDto>();
            _logger.LogInformation($"TicketDtos created with count {ticketDtos.Count}");

            var showtimeDto = new ShowtimeDto
            {
                Id = showtime.Id,
                StartTime = showtime.StartTime,
                Movie = movieDto,
                Tickets = ticketDtos
            };
            _logger.LogInformation($"ShowtimeDto created with ID {showtimeDto.Id}, StartTime {showtimeDto.StartTime}, Movie ID {showtimeDto.Movie.Id}, and {showtimeDto.Tickets.Count} tickets.");

            return Ok(showtimeDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the ShowtimeDto.");
            return StatusCode(500, "Internal server error");
        }
    }
}