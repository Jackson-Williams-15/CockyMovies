using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/movies
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieService.GetMovies();

            // map movie entities to movieDtos
            var movieDtos = movies.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                DateReleased = m.DateReleased,
                Genres = m.Genres.Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList(),
                ImageUrl = m.ImageUrl,
                Rating = m.Rating.Name
            }).ToList();

            return Ok(movieDtos);
        }

        // GET: api/movies/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }

            var movieDto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                DateReleased = movie.DateReleased,
                Genres = movie.Genres.Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList(),
                // showtimes added
                Showtimes = movie.Showtimes.Select(s => new ShowtimeDto
                {
                    Id = s.Id,
                    StartTime = s.StartTime,
                    Tickets = s.Tickets.Select(t => new TicketDto
                    {
                        Id = t.Id
                    }).ToList()
                }).ToList(),
                // ImageUrl in the response
                ImageUrl = movie.ImageUrl,
                Rating = movie.Rating.Name
            };

            return Ok(movieDto);
        }

        // POST: api/movies
        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] MovieCreateDto movieDto)
        {
            // genres by their IDs
            if (movieDto.GenreIds == null || !movieDto.GenreIds.Any())
            {
                return BadRequest("Genre IDs cannot be null or empty.");
            }

            var genres = await _movieService.GetGenresByIds(movieDto.GenreIds);

            if (!genres.Any())
            {
                return BadRequest("Invalid genre IDs provided.");
            }
            // map MovieCreateDto to Movie entity and related genres
            var movie = new Movie
            {
                Title = movieDto.Title,
                Description = movieDto.Description,
                DateReleased = movieDto.DateReleased,
                // assign the genres to the movie
                Genres = genres,
                // initialize Showtimes
                Showtimes = new List<Showtime>(),
                ImageUrl = movieDto.ImageUrl,
                RatingId = movieDto.RatingId
            };

            var success = await _movieService.AddMovie(movie);

            if (!success)
            {
                return BadRequest("A movie with the same ID already exists.");
            }

            return Ok("Movie added successfully.");
        }

        // DELETE: api/movies/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveMovie(int id)
        {
            var movie = await _movieService.GetMovieById(id);
            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            var success = await _movieService.RemoveMovie(movie);
            if (!success)
            {
                return BadRequest("Failed to remove the movie.");
            }

            return Ok("Movie removed successfully.");
        }
    }
}