using System;
using System.Collections.Generic;

namespace CE.Models;

public partial class Review
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? Rating { get; set; }

    public string? Description { get; set; }

    public int? MovieId { get; set; }

    public string? Movie { get; set; }
}
