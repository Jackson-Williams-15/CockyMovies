using CM.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CM.API.Interfaces;

/// <summary>
/// Interface for showtime service.
/// </summary>
public interface IShowtimeService
{
    /// <summary>
    /// Adds a new showtime.
    /// </summary>
    /// <param name="showtime">The showtime to add.</param>
    /// <seealso cref="CM.API.Services.ShowtimeService.AddShowtime(Showtime)"/>   
    Task<bool> AddShowtime(Showtime showtime);

    /// <summary>
    /// Edits an existing showtime.
    /// </summary>
    /// <param name="id">The ID of the showtime to edit.</param>
    /// <param name="editedShowtime">The edited showtime object.</param>
    /// <seealso cref="CM.API.Services.ShowtimeService.EditShowtime(int, Showtime)"/>
    Task<bool> EditShowtime(int id, Showtime editedShowtime);

    /// <summary>
    /// Gets showtimes by movie ID.
    /// </summary>
    /// <param name="movieId">The movie ID.</param>
    /// <seealso cref="CM.API.Services.ShowtimeService.GetShowtimesByMovieId(int)"/>
    Task<List<ShowtimeDto>> GetShowtimesByMovieId(int movieId);

    /// <summary>
    /// Removes a showtime by ID.
    /// </summary>
    /// <param name="id">The ID of the showtime to remove.</param>
    /// <seealso cref="CM.API.Services.ShowtimeService.RemoveShowtime(int)"/>
    Task<bool> RemoveShowtime(int id);
}
