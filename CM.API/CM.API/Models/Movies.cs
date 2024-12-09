namespace CM.API.Models;

/// <summary>
/// Represents a movie.
/// </summary>
public class Movie
{
    /// <summary>
    /// Gets or sets the movie ID.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the movie title.
    /// </summary>
    public required string Title { get; set; }
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
    public List<Genre>? Genres { get; set; }
    /// <summary>
    /// Gets or sets the list of showtimes for the movie.
    /// </summary>
    public List<Showtime>? Showtimes { get; set; }
    /// <summary>
    /// Gets or sets the list of reviews for the movie.
    /// </summary>
    public List<Review>? Reviews { get; set; }
    // Set default image URL
    private string _imageUrl = @"https://plus.unsplash.com/premium_vector-1682303466154-2161da750ac7?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MT
    l8fG1vdmllfGVufDB8fDB8fHww";

    /// <summary>
    /// Gets or sets the image URL for the movie.
    /// </summary>
    public string ImageUrl
    {
        get => string.IsNullOrWhiteSpace(_imageUrl) ? @"https://plus.unsplash.com/premium_vector-1682303466154-2161da750ac7?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxM
        jA3fDB8MHxzZWFyY2h8MTl8fG1vdmllfGVufDB8fDB8fHww" : _imageUrl;
        set => _imageUrl = value ?? @"https://plus.unsplash.com/premium_vector-1682303466154-2161da750ac7?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2
        h8MTl8fG1vdmllfGVufDB8fDB8fHww";
    }

    /// <summary>
    /// Gets or sets the rating ID for the movie.
    /// </summary>
    public int RatingId { get; set; }
    /// <summary>
    /// Gets or sets the rating for the movie.
    /// </summary>
    public Rating? Rating { get; set; }
}