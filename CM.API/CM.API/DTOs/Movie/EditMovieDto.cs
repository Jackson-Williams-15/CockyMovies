/// <summary>
/// Data transfer object for editing an existing movie.
/// </summary>
public class EditMovieDto
{
    /// <summary>
    /// Gets or sets the movie title.
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// Gets or sets the movie description.
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Gets or sets the release date of the movie.
    /// </summary>
    public DateTime DateReleased { get; set; }
    /// <summary>
    /// Gets or sets the list of genre IDs associated with the movie.
    /// </summary>
    public List<int>? GenreIds { get; set; }
    /// <summary>
    /// Gets or sets the image URL for the movie.
    /// </summary>
    public string? ImageUrl { get; set; }
    /// <summary>
    /// Gets or sets the rating ID for the movie.
    /// </summary>
    public int RatingId { get; set; }
}