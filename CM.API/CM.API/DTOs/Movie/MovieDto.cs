public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime DateReleased { get; set; }
    public List<GenreDto>? Genres { get; set; } = new List<GenreDto>();
    public List<ShowtimeDto> Showtimes { get; set; } = new List<ShowtimeDto>();
    public string? ImageUrl { get; set; }
    public string Rating { get; set; } = string.Empty;
    public double? AverageReviewRating { get; set; }
}