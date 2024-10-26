using CM.API.Models;
using System.Collections.Generic;

namespace CM.API.Interfaces;

public interface IGenreService
{
    List<Genre> GetGenres();
    Genre? GetGenreById(int id);
    bool AddGenre(Genre genre);
}
