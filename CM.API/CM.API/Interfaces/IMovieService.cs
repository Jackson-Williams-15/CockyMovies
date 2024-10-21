using CM.API.Models;

namespace CM.API.Interfaces;
public interface IMovieService
{
    Movie? GetMovie(int id);

    bool AddMovie(Movie movie);

    List<Movie> GetAllMovies();
    }