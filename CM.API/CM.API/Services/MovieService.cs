using CM.API.Interfaces;
using CM.API.Models;
using System.Collections.Generic;
using System.Linq;
using CM.API.Repositories;
namespace CM.API.Services
{
    public class MovieService : IMovieService
    {
        private readonly List<Movie> _movies = new List<Movie>();
        private readonly GenreRepository _genreRepository;

        public MovieService(GenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public bool AddMovie(Movie movie)
        {
            // if movie already exists
            if (_movies.Any(m => m.Id == movie.Id))
            {
                return false;
            }

            movie.Id = _movies.Count > 0 ? _movies.Max(m => m.Id) + 1 : 1;

            // add movie in-memory
            _movies.Add(movie);
            return true;
        }

        public Movie? GetMovieById(int id)
        {
            return _movies.FirstOrDefault(m => m.Id == id);
        }

        public List<Movie> GetMovies()
        {
            return _movies;
        }

        public List<Genre> GetGenresByIds(List<int> genreIds)
        {
            // Show in console the genre IDs 
            System.Console.WriteLine("Provided genre IDs: " + string.Join(", ", genreIds));

            var fetchedGenres = _genreRepository.GetGenres()
                .Where(g => genreIds.Contains(g.Id))
                .ToList();

            // Showing genre IDs being fetched for the movie create
            System.Console.WriteLine("Fetched Genres: " + string.Join(", ", fetchedGenres.Select(g => g.Name)));

            return fetchedGenres;
        }
}

}
