/// <summary>
/// Data transfer object for creating a new showtime.
/// </summary>
public class ShowtimeCreateDto
{
    /// <summary>
    /// Gets or sets the start time of the showtime.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Gets or sets the foreign key for the associated movie.
    /// </summary>
    public int MovieId { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of seats available for the showtime.
    /// </summary>
    public int Capacity { get; set; }
}