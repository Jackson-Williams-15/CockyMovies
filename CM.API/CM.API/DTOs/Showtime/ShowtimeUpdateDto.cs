using System.ComponentModel.DataAnnotations;

public class ShowtimeUpdateDto
{
    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public int MovieId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1.")]
    public int Capacity { get; set; }
}
