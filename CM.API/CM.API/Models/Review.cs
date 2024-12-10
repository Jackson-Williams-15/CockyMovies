namespace CM.API.Models;

/// <summary>
/// Represents a review for a movie.
/// </summary>
public class Review
{
    /// <summary>
    /// Gets or sets the review identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the review.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the rating of the review (out of 5).
    /// </summary>
    public int Rating { get; set; } // Rating out of 5

    /// <summary>
    /// Gets or sets the description of the review.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the movie identifier.
    /// </summary>
    public int MovieId { get; set; }

    /// <summary>
    /// Gets or sets the movie associated with the review.
    /// </summary>
    public Movie? Movie { get; set; } // Navigation property

    /// <summary>
    /// Gets or sets the username of the reviewer.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the number of likes for the review.
    /// </summary>
    public int Likes { get; set; }

    /// <summary>
    /// Gets or sets the collection of replies to the review.
    /// </summary>
    public ICollection<Reply>? Reply { get; set; }
}