using CM.API.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CM.API.Interfaces;

public interface IReviewService
{
    Task<bool> AddReview(Review review);
    Task<List<Review>> GetReviews(int movieId);
    Task<bool> EditReview(int reviewId, Review updatedReview);
}
