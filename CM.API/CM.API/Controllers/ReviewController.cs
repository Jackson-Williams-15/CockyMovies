using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    // POST: api/reviews
    [HttpPost]
    public async Task<IActionResult> AddReview([FromBody] ReviewCreateDto reviewDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var review = new Review
        {
            Title = reviewDto.Title,
            Rating = reviewDto.Rating,
            Description = reviewDto.Description,
            MovieId = reviewDto.MovieId
        };

        var success = await _reviewService.AddReview(review);

        if (!success)
        {
            return BadRequest("Failed to add review.");
        }

        return Ok("Review added successfully.");
    }

    // GET: api/reviews/movie/{movieId}
    [HttpGet("movie/{movieId}")]
    public async Task<IActionResult> GetReviews(int movieId)
    {
        var reviews = await _reviewService.GetReviews(movieId);

        if (!reviews.Any())
        {
            return NotFound("No reviews found for this movie.");
        }

        var reviewDtos = reviews.Select(r => new ReviewDto
        {
            Id = r.Id,
            Title = r.Title,
            Rating = r.Rating,
            Description = r.Description,
            MovieId = r.MovieId
        }).ToList();

        return Ok(reviewDtos);
    }

    // DELETE: api/reviews/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveReview(int id)
    {
        var success = await _reviewService.RemoveReview(id);

        if (!success)
        {
            return NotFound("Failed to remove review.");
        }

        return Ok("Review removed successfully.");
    }

}
