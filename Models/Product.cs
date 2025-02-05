using System;
using System.Collections.Generic;

namespace CLARO_IV_API.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public bool IsDeleted { get; set; }
}
