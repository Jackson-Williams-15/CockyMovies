using System.ComponentModel.DataAnnotations;

namespace CM.API.Models;
public class Showtime
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }

    // Foreign key for the associated movie
    public int MovieId { get; set; }

    [Required]
    public required Movie Movie { get; set; }

    [Required]
    public required ICollection<Ticket> Tickets { get; set; }

     // Max number of seats available
    [Required]
    public int Capacity { get; set; }
}
