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

            //Set the ImageUrl while adding the movie
            if (string.IsNullOrWhiteSpace(movie.ImageUrl))
            {
                movie.ImageUrl = "https://plus.unsplash.com/premium_vector-1682303466154-2161da750ac7?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTl8fG1vdmllfGVufDB8fDB8fHww";
            }
            // add movie in-memory
            _movies.Add(movie);
            return true;
        }

        public Movie? GetMovieById(int id)
        {
            var movie = _movies.FirstOrDefault(m => m.Id == id);
            if (movie != null)
            {
                // Return the movie with all properties
                return new Movie
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Description = movie.Description,
                    DateReleased = movie.DateReleased,
                    Genres = movie.Genres,
                    Showtimes = movie.Showtimes,
                    ImageUrl = movie.ImageUrl
                };
            }
            return null;
        }

        public List<Movie> GetMovies()
        {
            return _movies.Select(movie => new Movie
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                DateReleased = movie.DateReleased,
                Genres = movie.Genres,
                Showtimes = movie.Showtimes,
                ImageUrl = movie.ImageUrl
            }).ToList();
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
