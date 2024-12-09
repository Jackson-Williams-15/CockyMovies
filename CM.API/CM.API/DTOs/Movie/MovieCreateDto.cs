public class MovieCreateDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime DateReleased { get; set; }
    public List<int>? GenreIds { get; set; } // Just the list of genre IDs
    public string? ImageUrl { get; set; }
    public int RatingId { get; set; }
}
