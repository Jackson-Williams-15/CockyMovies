using CM.API.Interfaces;
using CM.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CM.API.Controllers;

/// <summary>
/// Controller for managing reviews.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReviewsController"/> class.
    /// </summary>
    /// <param name="reviewService">The review service.</param>
    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    /// <summary>
    /// Adds a new review.
    /// </summary>
    /// <param name="reviewDto">The review data transfer object.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
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
            MovieId = reviewDto.MovieId,
            Username = reviewDto.Username
        };

        var success = await _reviewService.AddReview(review);

        if (!success)
        {
            return BadRequest("Failed to add review.");
        }

        return Ok("Review added successfully.");
    }

    /// <summary>
    /// Gets reviews for a specific movie.
    /// </summary>
    /// <param name="movieId">The movie identifier.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
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
            MovieId = r.MovieId,
            Username = r.Username
        }).ToList();

        return Ok(reviewDtos);
    }
    [HttpPost("{reviewId}/like")]
    public async Task<IActionResult> LikeReview(int reviewId)
    {
        var success = await _reviewService.LikeReview(reviewId);

        if (!success)
        {
            return NotFound("Review not found.");
        }

        return Ok("Review liked successfully.");
    }


    /// <summary>
    /// Edits an existing review.
    /// </summary>
    /// <param name="id">The review identifier.</param>
    /// <param name="reviewDto">The review data transfer object.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
    // PUT: api/reviews/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> EditReview(int id, [FromBody] ReviewUpdateDto reviewDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedReview = new Review
        {
            Title = reviewDto.Title,
            Rating = reviewDto.Rating,
            Description = reviewDto.Description
        };

        var success = await _reviewService.EditReview(id, updatedReview);

        if (!success)
        {
            return NotFound("Review not found.");
        }

        return Ok("Review updated successfully.");
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
