using CM.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CM.API.Interfaces
{
    public interface IRatingService
    {
        // Asynchronously retrieves a list of all ratings.
        Task<List<Rating>> GetRatings();
    }
}
