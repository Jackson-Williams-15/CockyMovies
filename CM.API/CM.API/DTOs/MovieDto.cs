public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime DateReleased { get; set; }
    public List<GenreDto>? Genres { get; set; }
}
