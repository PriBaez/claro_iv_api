using System;
using System.Collections.Generic;

namespace CLARO_IV_API.Models;

public class ProductView
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public string PriceRange { get; set; } = null!;

    public List<string> Colors { get; set; } = new List<string>();

    public virtual Category? Category { get; set; } = null!;

    public virtual ICollection<ProductVariant>? ProductVariants { get; set; } = new List<ProductVariant>();
}
