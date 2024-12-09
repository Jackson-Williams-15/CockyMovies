using CM.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CM.API.Interfaces;

public interface IShowtimeService
{
    Task<bool> AddShowtime(Showtime showtime);
    Task<bool> EditShowtime(int id, Showtime editedShowtime);

    Task<List<ShowtimeDto>> GetShowtimesByMovieId(int movieId);
}
