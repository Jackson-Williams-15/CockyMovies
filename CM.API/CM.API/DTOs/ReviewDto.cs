public class ReviewDto
{
    // Unique identifier for the review.
    public int Id { get; set; }

    // Title of the review.
    public string? Title { get; set; }

    // Rating given in the review.
    public int Rating { get; set; }

    // Detailed description of the review.
    public string? Description { get; set; }

    // ID of the movie being reviewed.
    public int MovieId { get; set; }

    // Username of the person who wrote the review.
    public string? Username { get; set; }

    // Number of likes the review has received.
    public int Likes { get; set; }
}
