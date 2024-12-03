using System;
using System.Collections.Generic;

namespace CE.Models;

public partial class Orderticket
{
    public int OrderTicketId { get; set; }

    public int? TicketId { get; set; }

    public int? ShowtimeId { get; set; }

    public int? MovieId { get; set; }

    public string? Price { get; set; }

    public int? ShowtimesId { get; set; }

    public int? OrderResultId { get; set; }

    public int? Quantity { get; set; }
}
