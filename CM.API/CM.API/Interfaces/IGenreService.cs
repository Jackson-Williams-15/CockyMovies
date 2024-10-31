using CM.API.Models;
using System.Collections.Generic;

namespace CM.API.Interfaces;

public interface IGenreService
{
    Task<List<Genre>> GetGenres();
    Task<Genre?> GetGenreById(int id);
    Task<bool> AddGenre(Genre genre);
}
