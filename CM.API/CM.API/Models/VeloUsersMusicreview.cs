using System;
using System.Collections.Generic;

namespace CE.MYSQLMODELS;

public partial class VeloUsersMusicreview
{
    public int Id { get; set; }

    public int? Uid { get; set; }

    public int? Musicid { get; set; }

    public string? Musicname { get; set; }

    public int? Musicstars { get; set; }

    public string? Fullreview { get; set; }

    public string? Musictype { get; set; }

    public string? Musicgeneration { get; set; }

    public string? Authphone { get; set; }

    public string? Authemail { get; set; }

    public string? Authfax { get; set; }

    public int? Malestarid { get; set; }

    public int? Femalestarid { get; set; }

    public string? Reviewtime { get; set; }

    public int? Closed { get; set; }

    public int? Totalhits { get; set; }

    public sbyte? Playlist { get; set; }
}
