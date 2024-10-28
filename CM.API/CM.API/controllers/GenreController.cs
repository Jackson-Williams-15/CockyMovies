using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    // GET: api/genre
    [HttpGet]
    public IActionResult GetAllGenres()
    {
        var genres = _genreService.GetGenres();

        // Map genre entities to GenreDto
        var genreDtos = genres.Select(g => new GenreDto
        {
            Id = g.Id,
            Name = g.Name
        }).ToList();

        return Ok(genreDtos);
    }

    // GET: api/genre/{id}
    [HttpGet("{id}")]
    public IActionResult GetGenreById(int id)
    {
        var genre = _genreService.GetGenreById(id);

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

    // POST: api/genre
    [HttpPost]
    public IActionResult AddGenre([FromBody] GenreCreateDto genreDto)
    {
        // Map GenreCreateDto to genre entity
        var genre = new Genre
        {
            Name = genreDto.Name
        };

        var success = _genreService.AddGenre(genre);

        if (!success)
        {
            return BadRequest("A genre with the same name already exists.");
        }

        return Ok("Genre added successfully.");
    }
}

