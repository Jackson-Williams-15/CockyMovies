using System;
using System.Collections.Generic;

namespace CE.MYSQLMODELS;

public partial class VeloUsersMusic
{
    public int Id { get; set; }

    public string? Musicname { get; set; }

    public int? Muscid { get; set; }

    public string? Musicgeneration { get; set; }

    public string? Musictype { get; set; }

    public int? Maleleadid { get; set; }

    public int? Femaleleadid { get; set; }

    public string? Music1url1 { get; set; }

    public string? Music1url2 { get; set; }

    public string? Music3url3 { get; set; }

    public string? Youtubeurl { get; set; }
}
