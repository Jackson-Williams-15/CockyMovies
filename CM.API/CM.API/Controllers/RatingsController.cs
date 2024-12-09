using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        // Constructor that injects the IRatingService dependency.
        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        // GET: api/ratings
        // Retrieves all ratings from the IRatingService.
        [HttpGet]
        public async Task<IActionResult> GetRatings()
        {
            // Calls the service to get all ratings.
            var ratings = await _ratingService.GetRatings();

            // Returns the list of ratings in the response with a 200 OK status.
            return Ok(ratings);
        }
    }
}
