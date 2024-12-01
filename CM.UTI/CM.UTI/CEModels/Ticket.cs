using System;
using System.Collections.Generic;

namespace CE.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public string? Price { get; set; }

    public int? ShowtimeId { get; set; }

    public int? Quantity { get; set; }

    public byte? IsSold { get; set; }
}
