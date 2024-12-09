using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CM.API.Controllers
{
    /// <summary>
    /// Controller for managing movies.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoviesController"/> class.
        /// </summary>
        /// <param name="movieService">The movie service.</param>
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        /// <summary>
        /// Gets a list of movies.
        /// </summary>
        /// <param name="genreIds">Optional list of genre IDs to filter movies.</param>
        /// <param name="ratingIds">Optional list of rating IDs to filter movies.</param>
        /// <returns>A list of movies.</returns>
        // GET: api/movies
        [HttpGet]
        public async Task<IActionResult> GetMovies([FromQuery] List<int>? genreIds = null, [FromQuery] List<int>? ratingIds = null)
        {
            // fetch movies using the movie service
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

        /// <summary>
        /// Gets a movie by its ID.
        /// </summary>
        /// <param name="id">The movie ID.</param>
        /// <returns>The movie with the specified ID.</returns>
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
                // Showtimes added
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

        /// <summary>
        /// Adds a new movie.
        /// </summary>
        /// <param name="movieDto">The movie data transfer object.</param>
        /// <returns>A result indicating the success or failure of the adding the movie.</returns>
        // POST: api/movies
        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] MovieCreateDto movieDto)
        {
            // validate genres by their IDs
            if (movieDto.GenreIds == null || !movieDto.GenreIds.Any())
            {
                return BadRequest("Genre IDs cannot be null or empty.");
            }

            // Use the service
            var genres = await _movieService.GetGenresByIds(movieDto.GenreIds);

            // Malidate genres
            if (!genres.Any())
            {
                return BadRequest("Invalid genre IDs provided.");
            }
            // Map MovieCreateDto to Movie entity and related genres
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

        /// <summary>
        /// Removes a movie by its ID.
        /// </summary>
        /// <param name="id">The movie ID.</param>
        /// <returns>A result indicating the success or failure of removing the movie.</returns>
        // DELETE: api/movies/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveMovie(int id)
        {
            // First fetch the movie by id
            var movie = await _movieService.GetMovieById(id);
            if (movie == null)
            {
                return NotFound("Movie not found.");
            }
            // Delete the movie
            var success = await _movieService.RemoveMovie(movie);
            if (!success)
            {
                return BadRequest("Failed to remove the movie.");
            }

            return Ok("Movie removed successfully.");
        }

        /// <summary>
        /// Edits an existing movie.
        /// </summary>
        /// <param name="id">The movie ID.</param>
        /// <param name="editMovieDto">The movie data transfer object with updated information.</param>
        /// <returns>A result indicating the success or failure of editing the specified movie.</returns>
        // PUT: api/movies/edit/{id}
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditMovie(int id, [FromBody] EditMovieDto editMovieDto)
        {
            // Find the movie, check if not found
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