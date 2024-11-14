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
    }