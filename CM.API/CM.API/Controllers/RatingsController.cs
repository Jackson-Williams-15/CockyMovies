using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CM.API.Controllers;

/// <summary>
/// Provides API endpoints to manage ratings.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RatingsController : ControllerBase
{
    private readonly IRatingService _ratingService;

    /// <summary>
    /// Initializes a new instance of the <see cref="RatingsController"/> class.
    /// </summary>
    /// <param name="ratingService">The rating service.</param>
    public RatingsController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    /// <summary>
    /// Retrieves all ratings.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> containing the list of ratings.</returns>
    // GET: api/ratings
    [HttpGet]
    public async Task<IActionResult> GetRatings()
    {
        var ratings = await _ratingService.GetRatings();
        return Ok(ratings);
    }
}