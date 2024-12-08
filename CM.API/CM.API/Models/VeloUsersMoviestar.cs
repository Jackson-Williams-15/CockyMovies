using System;
using System.Collections.Generic;

namespace CE.MYSQLMODELS;

public partial class VeloUsersMoviestar
{
    public int Id { get; set; }

    public int? Starid { get; set; }

    public string? Starbirthdayyear { get; set; }

    public string? Starname { get; set; }

    public int? Otherwork1 { get; set; }

    public int? Otherwork2 { get; set; }

    public int? Otherwork3 { get; set; }

    public string? Pictureurl { get; set; }
}
