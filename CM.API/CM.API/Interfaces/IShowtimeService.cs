using CM.API.Models;  // Models like Showtime and ShowtimeDto
using Microsoft.AspNetCore.Mvc;  // MVC related namespaces

namespace CM.API.Interfaces
{
    public interface IShowtimeService
    {
        // Adds a new showtime to the system
        Task<bool> AddShowtime(Showtime showtime);

        // Edits an existing showtime by ID
        Task<bool> EditShowtime(int id, Showtime editedShowtime);

        // Retrieves a list of showtimes for a specific movie by movie ID
        Task<List<ShowtimeDto>> GetShowtimesByMovieId(int movieId);
        Task<bool> RemoveShowtime(int id);
    }
}
