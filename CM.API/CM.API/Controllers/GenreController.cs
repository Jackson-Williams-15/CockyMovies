using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CM.API.Controllers;

/// <summary>
/// Controller for managing genres.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenreController"/> class.
    /// </summary>
    /// <param name="genreService">The genre service.</param>
    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    /// <summary>
    /// Gets all genres.
    /// </summary>
    /// <returns>A list of genres.</returns>
    // GET: api/genre
    [HttpGet]
    public async Task<IActionResult> GetAllGenres()
    {
        var genres = await _genreService.GetGenres();

        // Map genre entities to GenreDto
        var genreDtos = genres.Select(g => new GenreDto
        {
            Id = g.Id,
            Name = g.Name
        }).ToList();

        return Ok(genreDtos);
    }


    /// <summary>
    /// Gets a genre by its identifier.
    /// </summary>
    /// <param name="id">The genre identifier.</param>
    /// <returns>The genre with the specified identifier.</returns>
    // GET: api/genre/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGenreById(int id)
    {
        var genre = await _genreService.GetGenreById(id);

        if (genre == null)
        {
            return NotFound();
        }

        var genreDto = new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name
        };

        return Ok(genreDto);
    }

    /// <summary>
    /// Adds a new genre.
    /// </summary>
    /// <param name="genreDto">The genre data transfer object.</param>
    /// <returns>A result indicating whether the genre was added successfully.</returns>
    // POST: api/genre
    [HttpPost]
    public async Task<IActionResult> AddGenre([FromBody] GenreCreateDto genreDto)
    {
        // Map GenreCreateDto to genre entity
        var genre = new Genre
        {
            Name = genreDto.Name
        };

        var success = await _genreService.AddGenre(genre);

        if (!success)
        {
            return BadRequest("A genre with the same name already exists.");
        }

        return Ok("Genre added successfully.");
    }
}