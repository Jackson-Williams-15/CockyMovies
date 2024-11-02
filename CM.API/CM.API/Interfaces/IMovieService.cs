using CM.API.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CM.API.Interfaces;
public interface IMovieService
{
    Task<Movie?> GetMovieById(int id);
    Task<bool> AddMovie(Movie movie);
    Task<bool> RemoveMovie(Movie movie);
    Task<List<Movie>> GetMovies();
    Task<List<Genre>> GetGenresByIds(List<int> genreIds);
}