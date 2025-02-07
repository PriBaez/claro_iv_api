using System;
using System.Collections.Generic;

namespace CLARO_IV_API.Models;

public partial class ProductVariant
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Color { get; set; } = null!;

    public int Stock { get; set; }

    public decimal Price { get; set; }

    public virtual Product? Product { get; set; } = null!;
}
