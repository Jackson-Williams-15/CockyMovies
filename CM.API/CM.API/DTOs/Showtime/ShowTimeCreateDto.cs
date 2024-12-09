public class ShowtimeCreateDto
{
    // Unique identifier for the showtime
    public int Id { get; set; }

    // Start time of the showtime
    public DateTime StartTime { get; set; }

    // ID of the movie associated with this showtime
    public int MovieId { get; set; }

    // Maximum number of available seats for this showtime
    public int Capacity { get; set; }
}
