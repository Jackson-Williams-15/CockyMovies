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

    /// <summary>
    /// Likes a review.
    /// See <see cref="CM.API.Services.ReviewService.LikeReview(int)"/> for the implementation.
    /// </summary>
    /// <param name="reviewId">The ID of the review to like.</param>
    Task<bool> LikeReview(int reviewId);

    /// <summary>
    /// Removes a review.
    /// See <see cref="CM.API.Services.ReviewService.RemoveReview(int)"/> for the implementation.
    /// </summary>
    /// <param name="reviewId">The ID of the review to remove.</param>
    Task<bool> RemoveReview(int reviewId);

    /// <summary>
    /// Gets a review by ID.
    /// See <see cref="CM.API.Services.ReviewService.GetReviewById(int)"/> for the implementation.
    /// </summary>
    /// <param name="reviewId">The ID of the review to retrieve.</param>
    Task<Review> GetReviewById(int reviewId);

    /// <summary>
    /// Gets replies for a specific review.
    /// See <see cref="CM.API.Services.ReviewService.GetReplies(int)"/> for the implementation.
    /// </summary>
    /// <param name="reviewId">The ID of the review to get replies for.</param>
    Task<List<Reply>> GetReplies(int reviewId);

    /// <summary>
    /// Adds a reply to a review.
    /// See <see cref="CM.API.Services.ReviewService.AddReply(Reply)"/> for the implementation.
    /// </summary>
    /// <param name="reply">The reply to add.</param>
    Task<bool> AddReply(Reply reply);

}
