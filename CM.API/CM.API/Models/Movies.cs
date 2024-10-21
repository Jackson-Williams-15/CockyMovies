namespace CM.API.Models;
    public class Movie
    {
        public int Id { get; set; } 

        public required string Title { get; set; }

        public string? Description { get; set; }

        public DateTime DateReleased { get; set; }

        public string? Genre { get; set; }
    }
