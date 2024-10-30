using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CM.API.Controllers;

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
    public IActionResult GetMovies()
    {
        var movies = _movieService.GetMovies();

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
            ImageUrl = m.ImageUrl
        }).ToList();

        return Ok(movieDtos);
    }

    // GET: api/movies/{id}
    [HttpGet("{id}")]
    public IActionResult GetMovieById(int id)
    {
        var movie = _movieService.GetMovieById(id);

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
            ImageUrl = movie.ImageUrl
        };

        return Ok(movieDto);
    }

    // POST: api/movies
    [HttpPost]
    public IActionResult AddMovie([FromBody] MovieCreateDto movieDto)
    {
        // genres by their IDs
        if (movieDto.GenreIds == null || !movieDto.GenreIds.Any())
        {
            return BadRequest("Genre IDs cannot be null or empty.");
        }

        var genres = _movieService.GetGenresByIds(movieDto.GenreIds);

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
            ImageUrl = movieDto.ImageUrl
        };

        var success = _movieService.AddMovie(movie);

        if (!success)
        {
            return BadRequest("A movie with the same ID already exists.");
        }

        return Ok("Movie added successfully.");
    }
}