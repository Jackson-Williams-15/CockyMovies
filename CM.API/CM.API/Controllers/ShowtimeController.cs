using CM.API.Data;  // Database context
using CM.API.Interfaces;  // Interfaces for services
using CM.API.Models;  // Models like Showtime, Movie, etc.
using Microsoft.AspNetCore.Mvc;  // ASP.NET Core MVC
using Microsoft.EntityFrameworkCore;  // Entity Framework Core
using System.Linq;  // LINQ queries
using System.Threading.Tasks;  // Asynchronous programming

namespace CM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowtimesController : ControllerBase
    {
        private readonly IShowtimeService _showtimeService;  // Showtime service
        private readonly IMovieService _movieService;  // Movie service
        private readonly AppDbContext _context;  // Database context
        private readonly ILogger<ShowtimesController> _logger;  // Logger for error handling

        // Constructor for injecting services and dependencies
        public ShowtimesController(IShowtimeService showtimeService, IMovieService movieService, AppDbContext context, ILogger<ShowtimesController> logger)
        {
            _showtimeService = showtimeService;
            _movieService = movieService;
            _context = context;
            _logger = logger;
        }

        // POST: api/showtimes (Add a new showtime)
        [HttpPost]
        public async Task<IActionResult> AddShowtime([FromBody] ShowtimeCreateDto showtimeDto)
        {
            var movie = await _movieService.GetMovieById(showtimeDto.MovieId);

            if (movie == null)
            {
                return NotFound("Movie not found.");
            }
            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            // Create new Showtime entity
            var showtime = new Showtime
            {
                StartTime = showtimeDto.StartTime,
                MovieId = showtimeDto.MovieId,
                Movie = movie,
                Capacity = showtimeDto.Capacity,
                Tickets = new List<Ticket>(),
            };

            // Add the showtime using the service
            var success = await _showtimeService.AddShowtime(showtime);

            if (!success)
            {
                return BadRequest("A showtime with the same ID already exists.");
            }
            if (!success)
            {
                return BadRequest("A showtime with the same ID already exists.");
            }

            return Ok("Showtime added successfully.");
        }
            return Ok("Showtime added successfully.");
        }

        // PUT: api/showtimes/{id} (Edit an existing showtime)
        [HttpPut("{id}")]
        public async Task<IActionResult> EditShowtime(int id, [FromBody] ShowtimeUpdateDto showtimeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            // Fetch the movie from the database to ensure the Movie property is set correctly
            var movie = await _movieService.GetMovieById(showtimeDto.MovieId);
            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            // Create the updated showtime entity
            var editedShowtime = new Showtime
            {
                StartTime = showtimeDto.StartTime,
                MovieId = showtimeDto.MovieId,
                Movie = movie,  // Ensure the Movie property is set
                Capacity = showtimeDto.Capacity,
                Tickets = new List<Ticket>()  // Initialize Tickets
            };

            try
            {
                var success = await _showtimeService.EditShowtime(id, editedShowtime);
            try
            {
                var success = await _showtimeService.EditShowtime(id, editedShowtime);

                if (!success)
                {
                    return NotFound("Showtime not found.");
                }
                if (!success)
                {
                    return NotFound("Showtime not found.");
                }

                return Ok("Showtime edited successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error editing showtime with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal server error while editing showtime.");
            }
        }
                return Ok("Showtime edited successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error editing showtime with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal server error while editing showtime.");
            }
        }

        // GET: api/showtimes/movie/{movieId} (Get all showtimes for a specific movie)
        [HttpGet("movie/{movieId}")]
        public async Task<IActionResult> GetShowtimesByMovieId(int movieId)
        {
            var showtimes = await _showtimeService.GetShowtimesByMovieId(movieId);

            if (showtimes == null || showtimes.Count == 0)
            {
                return NotFound("No showtimes found for the specified movie.");
            }
            if (showtimes == null || showtimes.Count == 0)
            {
                return NotFound("No showtimes found for the specified movie.");
            }

            return Ok(showtimes);
        }

        // GET: api/showtimes/{id} (Get details of a specific showtime)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShowtimeById(int id)
        {
            var showtime = await _context.Showtime
                .Include(s => s.Movie)  // Include movie details
                .Include(s => s.Tickets)  // Include ticket details
                .FirstOrDefaultAsync(s => s.Id == id);

            if (showtime == null)
            {
                _logger.LogWarning($"Showtime with ID {id} not found.");
                return NotFound("Showtime not found.");
            }
            if (showtime == null)
            {
                _logger.LogWarning($"Showtime with ID {id} not found.");
                return NotFound("Showtime not found.");
            }

            // Map the Showtime entity to ShowtimeDto for response
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

        // DELETE: api/showtimes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveShowtime(int id)
        {
            var success = await _showtimeService.RemoveShowtime(id);

            if (!success)
            {
                return NotFound("Showtime not found.");
            }

            return Ok("Showtime removed successfully.");
        }
    }
}