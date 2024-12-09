using System.ComponentModel.DataAnnotations;  // Data annotations for validation

public class ShowtimeUpdateDto
{
    // The start time for the showtime
    [Required]
    public DateTime StartTime { get; set; }

    // The ID of the movie for this showtime
    [Required]
    public int MovieId { get; set; }

    // Capacity of the showtime
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1.")]
    public int Capacity { get; set; }
}
