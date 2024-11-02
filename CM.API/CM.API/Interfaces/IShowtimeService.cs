using CM.API.Models;

namespace CM.API.Interfaces;

public interface IShowtimeService
{
    Task<bool> AddShowtime(Showtime showtime);
    Task<List<Showtime>> GetShowtimesByMovieId(int movieId);
}
