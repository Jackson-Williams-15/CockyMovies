using System.ComponentModel.DataAnnotations;

namespace CM.API.Models;
    public class Movie
    {
        public int Id { get; set; } 

        public required string Title { get; set; }

        public string? Description { get; set; }

        public DateTime DateReleased { get; set; }

        public List<Genre>? Genres { get; set; }

        [Required]
        public required List<Showtime> Showtimes {get; set;}
    }
