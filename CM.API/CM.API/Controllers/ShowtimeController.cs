using CM.API.Data;
using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CM.API.Controllers
{
    /// <summary>
    /// Controller for managing showtimes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ShowtimesController : ControllerBase
    {
        private readonly IShowtimeService _showtimeService;
        private readonly IMovieService _movieService;
        private readonly AppDbContext _context;
        private readonly ILogger<ShowtimesController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShowtimesController"/> class.
        /// </summary>
        /// <param name="showtimeService">The showtime service.</param>
        /// <param name="movieService">The movie service.</param>
        /// <param name="context">The database context.</param>
        /// <param name="logger">The logger.</param>
        public ShowtimesController(IShowtimeService showtimeService, IMovieService movieService, AppDbContext context, ILogger<ShowtimesController> logger)
        {
            _showtimeService = showtimeService;
            _movieService = movieService;
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Adds a new showtime.
        /// </summary>
        /// <param name="showtimeDto">The showtime data transfer object.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the adding the showtime.</returns>
        // POST: api/showtimes
        [HttpPost]
        public async Task<IActionResult> AddShowtime([FromBody] ShowtimeCreateDto showtimeDto)
        {
            // Fetch the movie id to add to
            var movie = await _movieService.GetMovieById(showtimeDto.MovieId);

            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            // Creates showtime object
            var showtime = new Showtime
            {
                StartTime = showtimeDto.StartTime,
                MovieId = showtimeDto.MovieId,
                Movie = movie,
                Capacity = showtimeDto.Capacity,
                Tickets = new List<Ticket>(),
            };

            // Add showtime using the service
            var success = await _showtimeService.AddShowtime(showtime);

            if (!success)
            {
                return BadRequest("A showtime with the same ID already exists.");
            }

            return Ok("Showtime added successfully.");
        }

        /// <summary>
        /// Edits an existing showtime.
        /// </summary>
        /// <param name="id">The showtime ID.</param>
        /// <param name="showtimeDto">The showtime data transfer object.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of editing the showtime.</returns>
        // PUT: api/showtimes/{id}
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

            // Create the edited showtime object
            var editedShowtime = new Showtime
            {
                StartTime = showtimeDto.StartTime,
                MovieId = showtimeDto.MovieId,
                Movie = movie,  // Set the Movie property explicitly
                Capacity = showtimeDto.Capacity,
                Tickets = new List<Ticket>()  // Explicitly initialize Tickets
            };

            // try and catch for editing the showtimes, throws exception if failed
            try
            {
                var success = await _showtimeService.EditShowtime(id, editedShowtime);

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

        /// <summary>
        /// Gets showtimes by movie ID.
        /// </summary>
        /// <param name="movieId">The movie ID.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the getting specified showtime by the relevant movie.</returns>
        // GET: api/showtimes/movie/{movieId}
        [HttpGet("movie/{movieId}")]
        public async Task<IActionResult> GetShowtimesByMovieId(int movieId)
        {
            var showtimes = await _showtimeService.GetShowtimesByMovieId(movieId);

            // Checks for showtimes in movie
            if (showtimes == null || showtimes.Count == 0)
            {
                return NotFound("No showtimes found for the specified movie.");
            }

            return Ok(showtimes);
        }

        /// <summary>
        /// Gets a showtime by ID.
        /// </summary>
        /// <param name="id">The showtime ID.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of getting specified showtime.</returns>
        // GET: api/showtimes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShowtimeById(int id)
        {
            // Fetch the showtime by ID, including related movie and tickets
            var showtime = await _context.Showtime
                .Include(s => s.Movie)
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (showtime == null)
            {
                _logger.LogWarning($"Showtime with ID {id} not found.");
                return NotFound("Showtime not found.");
            }

            // Map Showtime to DTO
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
                    DateReleased = showtime.Movie.DateReleased,
                },
                Tickets = showtime.Tickets.Select(t => new TicketDto
                {
                    Id = t.Id,
                    Price = t.Price
                }).ToList(),
                AvailableTickets = showtime.TicketsAvailable,
                Capacity = showtime.Capacity
            };

            return Ok(showtimeDto);
        }

        /// <summary>
        /// Gets a showtime by ID.
        /// </summary>
        /// <param name="id">The showtime ID.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of removing the showtime.</returns>
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