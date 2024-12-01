using System;
using System.Collections.Generic;

namespace CE.Models;

public partial class Moviegenre
{
    public int Id { get; set; }

    public int? GenresId { get; set; }

    public int? MoviesId { get; set; }

    public string? GenreName { get; set; }
}
