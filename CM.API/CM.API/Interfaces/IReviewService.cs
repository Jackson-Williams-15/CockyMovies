using CM.API.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CM.API.Interfaces;
/// <summary>
/// Interface for review service operations.
/// </summary>
public interface IReviewService
{
    /// <summary>
    /// Adds a new review to the database.
    /// See <see cref="ReviewService.AddReview(Review)"/> for the implementation.
    /// </summary>
    /// <param name="review">The review to add.</param>
    Task<bool> AddReview(Review review);
    /// <summary>
    /// Retrieves reviews from the database.
    /// See <see cref="ReviewService.GetReviews(int)"/> for the implementation.
    /// </summary>
    /// <param name="movieId">The ID of the movie.</param>
    Task<List<Review>> GetReviews(int movieId);
    /// <summary>
    /// Edits an existing review.
    /// See <see cref="ReviewService.EditReview(int, Review)"/> for the implementation.
    /// </summary>
    /// <param name="reviewId">The ID of the review to edit.</param>
    /// <param name="updatedReview">The updated review details.</param>
    Task<bool> EditReview(int reviewId, Review updatedReview);
    Task<bool> RemoveReview(int reviewId);
}
