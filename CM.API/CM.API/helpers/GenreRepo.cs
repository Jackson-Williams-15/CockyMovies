// GenreRepository.cs
using System.Collections.Generic;
using System.Linq;
using CM.API.Models;
namespace CM.API.Repositories;

// A hub of genres so movie and genre services can access.
public class GenreRepository
{
    private List<Genre> _genres = new List<Genre>();

    public List<Genre> GetGenres()
    {
        return _genres;
    }

    public Genre? GetGenreById(int id)
    {
        return _genres.FirstOrDefault(g => g.Id == id);
    }

    public bool AddGenre(Genre genre)
    {
        // Genre already exists
        if (_genres.Any(g => g.Name.ToLower() == genre.Name.ToLower()))
        {
            return false;
        }

        genre.Id = _genres.Count > 0 ? _genres.Max(g => g.Id) + 1 : 1;
        _genres.Add(genre);
        return true;
    }
}

