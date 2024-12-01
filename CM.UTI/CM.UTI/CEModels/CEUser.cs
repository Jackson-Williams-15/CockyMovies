using System;
using System.Collections.Generic;

namespace cleanweather.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Fullname { get; set; }

    public string? Email { get; set; }
}
