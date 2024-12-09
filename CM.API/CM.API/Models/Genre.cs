namespace CM.API.Models
{
    // Model representing a genre
    public class Genre
    {
        // Genre's unique identifier
        public int Id { get; set; }

        // Name of the genre
        public string Name { get; set; } = string.Empty;

        // List of movies associated with this genre
        public List<Movie> Movies { get; set; } = new();
    }
}
