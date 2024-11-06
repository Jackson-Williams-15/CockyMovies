public class ShowtimeDto
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public List<TicketDto> Tickets { get; set; } = new List<TicketDto>();
    public int AvailableTickets { get; set; }
    public decimal TicketPrice => Tickets.FirstOrDefault()?.Price ?? 0;
    public MovieDto Movie { get; set; }
}