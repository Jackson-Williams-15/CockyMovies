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

    public async Task<List<Genre>> GetGenres()
    {
        return await _genreRepository.GetGenres();
    }

    public async Task<Genre?> GetGenreById(int id)
    {
        return await _genreRepository.GetGenreById(id);
    }

    public async Task<bool> AddGenre(Genre genre)
    {
        return await _genreRepository.AddGenre(genre);
    }
}

