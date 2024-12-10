using System.ComponentModel.DataAnnotations;

/// <summary>
/// Data transfer object for updating a review.
/// </summary>
public class ReviewUpdateDto
{
    /// <summary>
    /// Gets or sets the title of the review.
    /// </summary>
    [StringLength(100, MinimumLength = 3)]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the rating of the review (out of 5).
    /// </summary>
    [Range(1, 5)]
    public int Rating { get; set; } // Rating out of 5

    /// <summary>
    /// Gets or sets the description of the review.
    /// </summary>
    [StringLength(1000, MinimumLength = 10)]
    public string? Description { get; set; }
}