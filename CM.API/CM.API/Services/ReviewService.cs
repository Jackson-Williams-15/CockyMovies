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

    public async Task<bool> AddReview(Review review)
    {
        try {
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return true;
        }
        catch(Exception ex) {
            _logger.LogError(ex, "Error adding review");
            throw;
        }
    }

    public async Task<List<Review>> GetReviews(int movieId)
    {
        return await _context.Reviews
            .Where(r => r.MovieId == movieId)
            .ToListAsync();
    }

    public async Task<bool> EditReview(int reviewId, Review updatedReview) 
    {
        try {
            var existingReview = await _context.Reviews.FindAsync(reviewId);
            if (existingReview == null) {
                return false;
            }

            existingReview.Title = updatedReview.Title;
            existingReview.Description = updatedReview.Description;
            existingReview.Rating = updatedReview.Rating;

            _context.Reviews.Update(existingReview);
            await _context.SaveChangesAsync();
            return true;
        } catch (Exception ex) {
            _logger.LogError(ex, "Error editing review");
            throw;
        }
    }
}
