namespace CM.API.Models;

public class Review
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int Rating { get; set; } // Rating out of 5
    public string? Description { get; set; }
    public int MovieId { get; set; }
    public Movie? Movie { get; set; } // Navigation property
}