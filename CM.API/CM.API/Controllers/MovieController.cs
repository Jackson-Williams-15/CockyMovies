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
        public async Task<IActionResult> GetMovies([FromQuery] List<int>? genreIds = null, [FromQuery] List<int>? ratingIds = null)
        {
            var movies = await _movieService.GetMovies(genreIds, ratingIds);

            // map movie entities to movieDtos
            var movieDtos = movies.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                DateReleased = m.DateReleased,
                Genres = m.Genres?.Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList(),
                ImageUrl = m.ImageUrl,
                Rating = m.Rating.Name,
                AverageReviewRating = m.Reviews != null && m.Reviews.Any() ? (double?)m.Reviews.Average(r => r.Rating) : null
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
                Rating = movie.Rating.Name,
                AverageReviewRating = movie.Reviews != null && movie.Reviews.Any() ? (double?)movie.Reviews.Average(r => r.Rating) : null
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

        // PUT: api/movies/edit
        // PUT: api/movies/edit/{id}
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditMovie(int id, [FromBody] EditMovieDto editMovieDto)
        {
            var oldMovie = await _movieService.GetMovieById(id);
            if (oldMovie == null)
            {
                return NotFound("Original movie not found.");
            }

            // Map EditMovieDto to Movie entity
            var newMovie = new Movie
            {
                Title = editMovieDto.Title,
                Description = editMovieDto.Description,
                DateReleased = editMovieDto.DateReleased,
                ImageUrl = editMovieDto.ImageUrl,
                RatingId = editMovieDto.RatingId,
                Genres = editMovieDto.GenreIds != null
                    ? await _movieService.GetGenresByIds(editMovieDto.GenreIds)
                    : new List<Genre>(),
                Showtimes = new List<Showtime>()
            };

            var success = await _movieService.EditMovie(oldMovie, newMovie);
            if (!success)
            {
                return BadRequest("Failed to update the movie.");
            }

            return Ok("Movie updated successfully.");
        }
    }
}