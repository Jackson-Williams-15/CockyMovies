// Importing the interface for the MovieService
using CM.API.Interfaces;
// Importing the models for movies, genres, and other DTOs
using CM.API.Models;
// Importing ASP.NET Core MVC components (controller, action result)
using Microsoft.AspNetCore.Mvc;
// Importing LINQ for working with collections
using System.Linq;
 // Importing the Task class for asynchronous programming
using System.Threading.Tasks;

namespace CM.API.Controllers
{
    // Defining the MoviesController class to handle HTTP requests related to movies.
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        // Dependency injection for the movie service
        private readonly IMovieService _movieService;

        // Constructor to inject the IMovieService dependency into the controller.
        public MoviesController(IMovieService movieService)
        {
            // Assign the injected service to the private field
            _movieService = movieService;
        }

        // GET: api/movies
        // Endpoint to get a list of movies, with optional query parameters for genre and rating filters.
        [HttpGet]
        public async Task<IActionResult> GetMovies([FromQuery] List<int>? genreIds = null, [FromQuery] List<int>? ratingIds = null)
        {
            // Fetch the list of movies based on optional filters for genres and ratings.
            var movies = await _movieService.GetMovies(genreIds, ratingIds);

            // Mapping the movie entities to MovieDto to send a simpler response.
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

            // Return the movie DTOs as a JSON response with an HTTP 200 OK status.
            return Ok(movieDtos);
        }

        // GET: api/movies/{id}
        // Endpoint to get a specific movie by its ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            // Fetch the movie with its related data (genres, showtimes, tickets, and rating).
            var movie = await _movieService.GetMovieById(id);

            // If the movie is not found, return a 404 Not Found status.
            if (movie == null)
            {
                return NotFound();
            }

            // Map the movie and related entities (genres, showtimes, tickets) to a MovieDto for the response.
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
                // Mapping showtimes and their tickets to DTOs
                Showtimes = movie.Showtimes.Select(s => new ShowtimeDto
                {
                    Id = s.Id,
                    StartTime = s.StartTime,
                    Tickets = s.Tickets.Select(t => new TicketDto
                    {
                        Id = t.Id
                    }).ToList()
                }).ToList(),
                ImageUrl = movie.ImageUrl,
                Rating = movie.Rating.Name,
                AverageReviewRating = movie.Reviews != null && movie.Reviews.Any() ? (double?)movie.Reviews.Average(r => r.Rating) : null
            };

            // Return the movie DTO with an HTTP 200 OK status.
            return Ok(movieDto);
        }

        // POST: api/movies
        // Endpoint to create a new movie.
        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] MovieCreateDto movieDto)
        {
            // Check if genreIds are provided and are not empty.
            if (movieDto.GenreIds == null || !movieDto.GenreIds.Any())
            {
                return BadRequest("Genre IDs cannot be null or empty.");
            }

            // Fetch the genres by their IDs from the movie service.
            var genres = await _movieService.GetGenresByIds(movieDto.GenreIds);

            // If no valid genres were found, return a 400 Bad Request.
            if (!genres.Any())
            {
                return BadRequest("Invalid genre IDs provided.");
            }

            // Map the MovieCreateDto to a Movie entity and assign related genres.
            var movie = new Movie
            {
                Title = movieDto.Title,
                Description = movieDto.Description,
                DateReleased = movieDto.DateReleased,
                Genres = genres,
                Showtimes = new List<Showtime>(),
                ImageUrl = movieDto.ImageUrl,
                RatingId = movieDto.RatingId
            };

            // Check if the movie was successfully added using the movie service.
            var success = await _movieService.AddMovie(movie);

            // If the movie was not added successfully (duplicate ID), return a 400 Bad Request.
            if (!success)
            {
                return BadRequest("A movie with the same ID already exists.");
            }

            // Return a success message with an HTTP 200 OK status.
            return Ok("Movie added successfully.");
        }

        // DELETE: api/movies/{id}
        // Endpoint to delete a movie by its ID.
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveMovie(int id)
        {
            // Retrieve the movie by its ID to check if it exists.
            var movie = await _movieService.GetMovieById(id);

            // If the movie does not exist, return a 404 Not Found status.
            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            // Attempt to remove the movie using the movie service.
            var success = await _movieService.RemoveMovie(movie);

            // If the removal was not successful, return a 400 Bad Request.
            if (!success)
            {
                return BadRequest("Failed to remove the movie.");
            }

            // Return a success message with an HTTP 200 OK status.
            return Ok("Movie removed successfully.");
        }

        // PUT: api/movies/edit
        // PUT: api/movies/edit/{id}
        // Endpoint to update an existing movie's details.
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditMovie(int id, [FromBody] EditMovieDto editMovieDto)
        {
            // Get the existing movie by its ID (old movie).
            var oldMovie = await _movieService.GetMovieById(id);

            // If the original movie is not found, return a 404 Not Found status.
            if (oldMovie == null)
            {
                return NotFound("Original movie not found.");
            }

            // Map newMovieDto to Movie entity
            var newMovie = new Movie
            {
                Title = editMovieDto.NewMovie.Title,
                Description = editMovieDto.NewMovie.Description,
                DateReleased = editMovieDto.NewMovie.DateReleased,
                ImageUrl = editMovieDto.NewMovie.ImageUrl,
                RatingId = editMovieDto.NewMovie.RatingId,
                Genres = editMovieDto.NewMovie.GenreIds != null
                    ? await _movieService.GetGenresByIds(editMovieDto.NewMovie.GenreIds)
                    : new List<Genre>()
            };

            // Attempt to edit the movie using the movie service.
            var success = await _movieService.EditMovie(oldMovie, newMovie);

            // If editing was unsuccessful, return a 400 Bad Request.
            if (!success)
            {
                return BadRequest("Failed to update the movie.");
            }

            // Return a success message with an HTTP 200 OK status.
            return Ok("Movie updated successfully.");
        }
    }
}
