using CM.API.Models;

namespace CM.API.Interfaces;
public interface IMovieService
{
    Movie? GetMovieById(int id);

    bool AddMovie(Movie movie);
    bool RemoveMovie(Movie movie);

    List<Movie> GetMovies();
    List<Genre> GetGenresByIds(List<int> genreIds);
}