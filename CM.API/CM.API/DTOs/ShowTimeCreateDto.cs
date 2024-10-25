public class ShowtimeCreateDto
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }

    // Key to movie
    public int MovieId { get; set; }
}