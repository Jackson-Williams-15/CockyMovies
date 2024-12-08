using System;
using System.Collections.Generic;

namespace CE.MYSQLMODELS;

public partial class VeloUsersMovie
{
    public int Id { get; set; }

    public string? Moviename { get; set; }

    public int? Movieid { get; set; }

    public string? Moviegeneration { get; set; }

    public string? Movietype { get; set; }

    public int? Maleleadid { get; set; }

    public int? Femaleleadid { get; set; }

    public string? Moviepicture1url { get; set; }

    public string? Moviepicture2url { get; set; }

    public string? Moviepicture3url { get; set; }

    public string? Youtubeurl { get; set; }
}
