using CM.API.Interfaces;
using CM.API.Models;
using System.Collections.Generic;
using System.Linq;
using CM.API.Repositories;
namespace CM.API.Services;

public class GenreService : IGenreService
{
    private readonly GenreRepository _genreRepository;

    public GenreService(GenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public List<Genre> GetGenres()
    {
        return _genreRepository.GetGenres();
    }

    public Genre? GetGenreById(int id)
    {
        return _genreRepository.GetGenreById(id);
    }

    public bool AddGenre(Genre genre)
    {
        return _genreRepository.AddGenre(genre);
    }
}

