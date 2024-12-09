// DTO representing a movie in the system.
public class MovieDto
{
    // The unique identifier of the movie.
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }

    // The release date of the movie.
    public DateTime DateReleased { get; set; }

    // A list of genre DTOs representing the genres associated with the movie.
    public List<GenreDto>? Genres { get; set; } = new List<GenreDto>();

    // A list of showtime DTOs representing showtimes for the movie.
    public List<ShowtimeDto> Showtimes { get; set; } = new List<ShowtimeDto>();

    // The URL of the movie's poster image.
    public string? ImageUrl { get; set; }

    // The name of the movie's rating (e.g., "PG", "R", "G").
    public string Rating { get; set; } = string.Empty;
    public double? AverageReviewRating { get; set; }
}
