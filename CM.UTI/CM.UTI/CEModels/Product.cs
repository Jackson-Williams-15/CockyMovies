using System;
using System.Collections.Generic;

namespace CE.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Prodid { get; set; }

    public string? Productdescr { get; set; }

    public double? Productmrc { get; set; }

    public double? Productnrc { get; set; }

    public double? Discount1 { get; set; }

    public double? Discount2 { get; set; }

    public double? Discount3 { get; set; }

    public string? Discountid { get; set; }
}
