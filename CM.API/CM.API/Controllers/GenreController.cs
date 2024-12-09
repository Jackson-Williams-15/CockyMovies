using CM.API.Interfaces;  // Service interfaces like IGenreService
using CM.API.Models;      // Models like Genre and DTOs
using Microsoft.AspNetCore.Mvc;  // ASP.NET Core MVC attributes and helpers
using System.Linq;        // LINQ for collection manipulation
using System.Threading.Tasks;  // Asynchronous programming

namespace CM.API.Controllers
{
    [ApiController]  // Marks the class as an API controller
    [Route("api/[controller]")]  // Sets the route for API requests (e.g., api/genre)
    public class GenreController : ControllerBase
    {
        // Service for handling genre operations
        private readonly IGenreService _genreService;

        // Constructor to inject IGenreService
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // GET: api/genre
        [HttpGet]  
        public async Task<IActionResult> GetAllGenres()
        {
            // Fetch all genres from the service
            var genres = await _genreService.GetGenres();

            // Convert genre entities to GenreDto for response
            var genreDtos = genres.Select(g => new GenreDto
            {
                Id = g.Id,
                Name = g.Name
            }).ToList();

            return Ok(genreDtos);  // Return genres as a list of GenreDto
        }

        // GET: api/genre/{id}
        [HttpGet("{id}")]  
        public async Task<IActionResult> GetGenreById(int id)
        {
            // Fetch a genre by its ID
            var genre = await _genreService.GetGenreById(id);

            if (genre == null)
            {
                return NotFound();  // Return 404 if genre is not found
            }

            // Convert the genre entity to GenreDto
            var genreDto = new GenreDto
            {
                Id = genre.Id,
                Name = genre.Name
            };

            return Ok(genreDto);  // Return the genre as a GenreDto
        }

        // POST: api/genre
        [HttpPost]
        public async Task<IActionResult> AddGenre([FromBody] GenreCreateDto genreDto)
        {
            // Map GenreCreateDto to Genre entity
            var genre = new Genre
            {
                Name = genreDto.Name
            };

            // Add the genre through the service
            var success = await _genreService.AddGenre(genre);

            if (!success)
            {
                return BadRequest("A genre with the same name already exists.");
            }

            return Ok("Genre added successfully.");
        }
    }
}
