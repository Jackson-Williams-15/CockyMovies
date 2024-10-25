using CM.API.Models;

namespace CM.API.Interfaces;

    public interface IShowtimeService
    {
        bool AddShowtime(Showtime showtime);
        List<Showtime> GetShowtimesByMovieId(int movieId);
    }
