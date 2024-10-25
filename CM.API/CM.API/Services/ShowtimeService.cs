using CM.API.Interfaces;
using CM.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace CM.API.Services;
    public class ShowtimeService : IShowtimeService
    {
        private readonly List<Showtime> _showtimes;
        private readonly IMovieService _movieService;
        private int _nextId;

        public ShowtimeService(IMovieService movieService)
        {
            _showtimes = new List<Showtime>();
            _movieService = movieService;
            _nextId = 1;
        }

        public bool AddShowtime(Showtime showtime)
        {
            if (_showtimes.Any(s => s.Id == showtime.Id))
            {
                return false;
            }

            showtime.Id = _nextId++;
            _showtimes.Add(showtime);

            // Update movie's showtimes list
            var movie = _movieService.GetMovieById(showtime.MovieId);
            if (movie != null)
            {
                movie.Showtimes.Add(showtime);
            }

            return true;
        }

        public List<Showtime> GetShowtimesByMovieId(int movieId)
        {
            return _showtimes.Where(s => s.MovieId == movieId).ToList();
        }
    }