using System;
using System.Collections.Generic;

namespace CE.MYSQLMODELS;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }
}
