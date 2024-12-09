using System;
using System.Collections.Generic;

namespace CE.MYSQLMODELS;

public partial class VeloUsersMoviereview
{
    public int Id { get; set; }

    public int? Uid { get; set; }

    public int? Movieid { get; set; }

    public string? Moviename { get; set; }

    public int? Moviestars { get; set; }

    public string? Fullreview { get; set; }

    public string? Movietype { get; set; }

    public string? Moviegeneration { get; set; }

    public string? Authphone { get; set; }

    public string? Authemail { get; set; }

    public string? Authfax { get; set; }

    public int? Malestarid { get; set; }

    public int? Femalestarid { get; set; }

    public string? Reviewtime { get; set; }

    public int? Closed { get; set; }

    public int? Totalhits { get; set; }
}
