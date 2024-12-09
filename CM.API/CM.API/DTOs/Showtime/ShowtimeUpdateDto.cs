using System.ComponentModel.DataAnnotations;

/// <summary>
/// Data transfer object for updating an existing showtime.
/// </summary>
public class ShowtimeUpdateDto
{
    /// <summary>
    /// Gets or sets the start time of the showtime.
    /// </summary>
    [Required]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Gets or sets the foreign key for the associated movie.
    /// </summary>
    [Required]
    public int MovieId { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of seats available for the showtime.
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1.")]
    public int Capacity { get; set; }
}
