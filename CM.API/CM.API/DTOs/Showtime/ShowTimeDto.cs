public class ShowtimeDto
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public List<TicketDto> Tickets { get; set; }
    public int AvailableTickets => Tickets.Count;
    public decimal TicketPrice => Tickets.FirstOrDefault()?.Price ?? 0;
}