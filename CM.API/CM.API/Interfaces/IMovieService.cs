using CM.API.Models;

namespace CM.API.Interfaces;
public interface IMovieService
{
    Movie? GetMovieById(int id);

    bool AddMovie(Movie movie);

    List<Movie> GetMovies();
    }