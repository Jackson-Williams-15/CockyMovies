public class EditMovieDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime DateReleased { get; set; }
    public List<int>? GenreIds { get; set; }
    public string? ImageUrl { get; set; }
    public int RatingId { get; set; }
}