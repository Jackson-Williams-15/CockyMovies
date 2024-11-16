using CM.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CM.API.Interfaces;

public interface IRatingService
{
    Task<List<Rating>> GetRatings();
}