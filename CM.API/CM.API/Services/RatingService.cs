using CM.API.Interfaces;
using CM.API.Models;
using CM.API.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CM.API.Services;

/// <summary>
/// Provides functionality to manage ratings.
/// </summary>
public class RatingService : IRatingService
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="RatingService"/> class.
    /// </summary>
    /// <param name="context">The application database context.</param>
    public RatingService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all ratings from the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of ratings.</returns>
    public async Task<List<Rating>> GetRatings()
    {
        return await _context.Ratings.ToListAsync();
    }
}