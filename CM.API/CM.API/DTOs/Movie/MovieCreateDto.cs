// DTO used for creating a new movie.
public class MovieCreateDto
{
    // The unique identifier of the movie.
    public int Id { get; set; }

    // The title of the movie.
    public string Title { get; set; }

    // A brief description of the movie.
    public string? Description { get; set; }

    // The release date of the movie.
    public DateTime DateReleased { get; set; }

    // A list of genre IDs associated with the movie.
    public List<int>? GenreIds { get; set; }

    // An optional image URL for the movie.
    public string? ImageUrl { get; set; }

    // The rating ID associated with the movie (e.g., G, PG, R).
    public int RatingId { get; set; }
}
