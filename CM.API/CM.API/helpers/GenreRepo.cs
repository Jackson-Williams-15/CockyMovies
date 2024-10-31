using System.Collections.Generic;
using System.Linq;
using CM.API.Models;
using CM.API.Data;

namespace CM.API.Repositories;
public class GenreRepository
{
    private readonly AppDbContext _context;

    public GenreRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Genre> GetGenres()
    {
        return _context.Genres.ToList();
    }

    public Genre? GetGenreById(int id)
    {
        return _context.Genres.FirstOrDefault(g => g.Id == id);
    }

    public bool AddGenre(Genre genre)
    {
        // Genre already exists
        if (_context.Genres.Any(g => g.Name.ToLower() == genre.Name.ToLower()))
        {
            return false;
        }

        _context.Genres.Add(genre);
        _context.SaveChanges();
        return true;
    }
}