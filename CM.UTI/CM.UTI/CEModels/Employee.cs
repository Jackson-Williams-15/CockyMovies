using System;
using System.Collections.Generic;

namespace cleanweather.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string? Fullname { get; set; }

    public string? Email { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? City { get; set; }

    public string? Regionstate { get; set; }

    public int? Zipcode { get; set; }
}
