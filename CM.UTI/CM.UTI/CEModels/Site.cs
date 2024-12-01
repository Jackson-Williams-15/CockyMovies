using System;
using System.Collections.Generic;

namespace cleanweather.Models;

public partial class Site
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public int? Siteid { get; set; }
}
