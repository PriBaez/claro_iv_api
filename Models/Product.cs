﻿using System;
using System.Collections.Generic;

namespace CLARO_IV_API.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public virtual Category? Category { get; set; } = null!;

    public virtual ICollection<ProductVariant>? ProductVariants { get; set; } = new List<ProductVariant>();
}
