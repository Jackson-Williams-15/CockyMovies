using CM.API.Interfaces;
using CM.API.Models;
using System.Collections.Generic;
using System.Linq;
using CM.API.Repositories;
using CM.API.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace CM.API.Services;
public class RatingService : IRatingService
{
    private readonly AppDbContext _context;
    
    private readonly RatingsRepository _ratingsRepository;

    public RatingService(RatingsRepository ratingsRepository)
    {
        _ratingsRepository = ratingsRepository;
    }
       

    public async Task<List<Rating>> GetRatings()
    {
        return await _context.Ratings.ToListAsync();
    }
  
    public async Task<Rating?> GetRatingsById(int id)
    {
        return await _ratingsRepository.GetRatingsById(id);
    }

    public async Task<bool> AddRatings(Rating rating)
    {
        return await _ratingsRepository.AddRating(rating);
    }
}