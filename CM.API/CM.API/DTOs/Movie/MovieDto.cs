/// <summary>
/// Data transfer object for a movie.
/// </summary>
public class MovieDto
{
    /// <summary>
    /// Gets or sets the movie ID.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the movie title.
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// Gets or sets the movie description.
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Gets or sets the release date of the movie.
    /// </summary>
    public DateTime DateReleased { get; set; }
    /// <summary>
    /// Gets or sets the list of genres associated with the movie.
    /// </summary>
    public List<GenreDto>? Genres { get; set; } = new List<GenreDto>();
    /// <summary>
    /// Gets or sets the list of showtimes for the movie.
    /// </summary>
    public List<ShowtimeDto> Showtimes { get; set; } = new List<ShowtimeDto>();
    /// <summary>
    /// Gets or sets the image URL for the movie.
    /// </summary>
    public string? ImageUrl { get; set; }
    /// <summary>
    /// Gets or sets the rating for the movie.
    /// </summary>
    public string Rating { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the average review rating for the movie.
    /// </summary>
    public double? AverageReviewRating { get; set; }
}