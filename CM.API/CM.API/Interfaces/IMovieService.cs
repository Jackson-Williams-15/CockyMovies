using CM.API.Models; // Importing the Movie and Genre models
using System.Threading.Tasks; // To support asynchronous methods
using System.Collections.Generic; // To work with collections like List<T>

namespace CM.API.Interfaces
{
    // Interface that defines the contract for movie-related operations.
    public interface IMovieService
    {
        // Asynchronously retrieves a movie by its unique ID. 
        // Returns a Movie object if found, or null if not found.
        Task<Movie?> GetMovieById(int id);

        // Asynchronously adds a new movie to the database.
        // Returns a boolean value indicating whether the movie was successfully added.
        Task<bool> AddMovie(Movie movie);

        // Asynchronously removes a movie from the database.
        // Returns a boolean value indicating whether the movie was successfully removed.
        Task<bool> RemoveMovie(Movie movie);

        // Asynchronously updates an existing movie's details.
        // Returns a boolean value indicating whether the movie was successfully edited.
        Task<bool> EditMovie(Movie oldMovie, Movie newMovie);

        // Asynchronously retrieves a list of movies, with optional filtering by genre and rating.
        // Returns a list of movies that match the given genre and/or rating criteria.
        Task<List<Movie>> GetMovies(List<int>? genreIds = null, List<int>? ratingIds = null);

        // Asynchronously retrieves a list of genres by their unique IDs.
        // Takes a list of genreIds and returns a list of matching Genre objects.
        Task<List<Genre>> GetGenresByIds(List<int> genreIds);
    }
}
