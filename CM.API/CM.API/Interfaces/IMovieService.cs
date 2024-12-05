using CM.API.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CM.API.Interfaces;
public interface IMovieService
{
    Task<Movie?> GetMovieById(int id);
    Task<bool> AddMovie(Movie movie);
    Task<bool> RemoveMovie(Movie movie);
    Task<bool> EditMovie(Movie oldMovie, Movie newMovie);

    Task<List<Movie>> GetMovies(List<int>? genreIds = null, List<int>? ratingIds = null);
    Task<List<Genre>> GetGenresByIds(List<int> genreIds);
}