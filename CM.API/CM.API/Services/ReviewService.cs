using CM.API.Interfaces;
using CM.API.Models;
using CM.API.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CM.API.Services;

public class ReviewService : IReviewService
{
    private readonly AppDbContext _context;
    private readonly ILogger<ReviewService> _logger;

    public ReviewService(AppDbContext context, ILogger<ReviewService> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Adds a new review to the database.
    /// </summary>
    /// <param name="review">The review to add.</param>
    /// <returns>A task result containing a 
    /// boolean showing whether the operation was successful.
    /// </returns>
    public async Task<bool> AddReview(Review review)
    {
        try
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return true;
        try
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding review");
            throw;
        }
    }
    /// <summary>
    /// Retrieves all reviews for a specific movie.
    /// </summary>
    /// <param name="movieId">The ID of the movie.</param>
    /// <returns>A task that results has a list of reviews for the 
    /// specified movie.</returns>
    public async Task<List<Review>> GetReviews(int movieId)
    {
        return await _context.Reviews
            .Where(r => r.MovieId == movieId)
            .ToListAsync();
    }
    /// <summary>
    /// Edits an existing review.
    /// </summary>
    /// <param name="reviewId">The ID of the review to edit.</param>
    /// <param name="updatedReview">The updated review details.</param>
    /// <returns>A  task result that has a boolean showing whether 
    /// the operation was successful.</returns>
    public async Task<bool> EditReview(int reviewId, Review updatedReview)
    {
        try
        {
            var existingReview = await _context.Reviews.FindAsync(reviewId);
            if (existingReview == null)
            {
                return false;
            }

            existingReview.Title = updatedReview.Title;
            existingReview.Description = updatedReview.Description;
            existingReview.Rating = updatedReview.Rating;

            _context.Reviews.Update(existingReview);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error editing review");
            throw;
        }
    }

    public async Task<bool> LikeReview(int reviewId)
    {
        var review = await _context.Reviews.FindAsync(reviewId);
        if (review == null)
        {
            return false;
        }

        review.Likes += 1;

        try
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error liking review");
            return false;
        }
    }

    public async Task<bool> AddReply(Reply reply)
{
    try
    {
        _context.Reply.Add(reply);
        await _context.SaveChangesAsync();
        return true;
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error adding reply");
        throw;
    }
}

    public async Task<List<Reply>> GetReplies(int reviewId)
    {
    return await _context.Reply
        .Where(r => r.ReviewId == reviewId)
        .ToListAsync();
    }

    public async Task<bool> RemoveReview(int reviewId)
    {
        try
        {
            var review = await _context.Reviews.FindAsync(reviewId);

            if (review == null)
            {
                return false;
            }
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing review.");
            throw;
        }
    }

    public async Task<Review> GetReviewById(int reviewId)
    {
        var review = await _context.Reviews.FindAsync(reviewId);
        return review ?? new Review();
    }
}
