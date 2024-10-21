using CM.API.Interfaces;
using CM.API.Models;

namespace CM.API.Services;

public class MovieService : IMovieService {

    private readonly List<Movie> _movies = new List<Movie>();
   public bool AddMovie(Movie movie)
        {
            if (_movies.Any(m => m.Id == movie.Id))
    {
        return false;
    }

    movie.Id = _movies.Count > 0 ? _movies.Max(m => m.Id) + 1 : 1;

    _movies.Add(movie);
    return true;
        }
        public Movie? GetMovieById(int id)
        {
            // find the movie with the specified ID
            var movie = _movies.FirstOrDefault(m => m.Id == id);

            // if the movie is found, return it
            return movie;
        }
        
        public List<Movie> GetMovies()
{
    return _movies;
}

    }
