using System;
using System.Collections.Generic;

namespace CE.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int? TicketId { get; set; }

    public int? UserId { get; set; }

    public int? CartId { get; set; }
}
