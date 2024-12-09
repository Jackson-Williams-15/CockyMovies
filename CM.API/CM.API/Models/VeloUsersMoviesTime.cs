using System;
using System.Collections.Generic;

namespace CE.MYSQLMODELS;

public partial class VeloUsersMoviesTime
{
    public int Id { get; set; }

    public string? Moviename { get; set; }

    public int? Movieid { get; set; }

    public string? Timesone { get; set; }

    public string? Timestwo { get; set; }

    public string? Timesthree { get; set; }

    public string? Timesfour { get; set; }

    public string? Timesfive { get; set; }

    public string? Timessix { get; set; }

    public string? Timesseven { get; set; }

    public string? Moviepicture1url { get; set; }

    public string? Moviepicture2url { get; set; }

    public string? Moviepicture3url { get; set; }

    public string? Youtubeurl { get; set; }
}
