using CM.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CM.API.Interfaces;

/// <summary>
/// Defines the contract for rating services.
/// </summary>
public interface IRatingService
{
    /// <summary>
    /// Retrieves all ratings.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of ratings.</returns>
    Task<List<Rating>> GetRatings();
}