using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatingsController : ControllerBase
{
    private readonly IRatingService _ratingService;

    public RatingsController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    // GET: api/ratings
    [HttpGet]
    public async Task<IActionResult> GetRatings()
    {
        var ratings = await _ratingService.GetRatings();
        return Ok(ratings);
    }

   //POST: api/ratings
    [HttpPost]
    public async Task<IActionResult> AddRatings([FromBody] RatingsCreateDto ratingsDto)
    {
        // Map RatingsCreateDto to ratings entity
        var ratings = new Rating
        {
            Name = RatingsDto.Name
        };

        var success = await _ratingService.AddRatings(ratings);

        if (!success)
        {
            return BadRequest("A rating with the same name already exists.");
        }

        return Ok("Rating added successfully.");
    }
}
