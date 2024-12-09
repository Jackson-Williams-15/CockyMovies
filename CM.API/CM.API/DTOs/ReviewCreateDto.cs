using System.ComponentModel.DataAnnotations;

public class ReviewCreateDto
{
    // Title of the review.
    [StringLength(100, MinimumLength = 3)]
    public string? Title { get; set; }

    // Rating of the review.
    [Range(1, 5)]
    public int Rating { get; set; }

    // Description of the review.
    [StringLength(1000, MinimumLength = 10)]
    public string? Description { get; set; }

    // ID of the movie being reviewed.
    [Required]
    public int MovieId { get; set; }
}
