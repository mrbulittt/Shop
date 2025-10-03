using System;
using System.Collections.Generic;

namespace Shop.Data;

public partial class ProdCategory
{
    public int IdCategory { get; set; }

    public string? NameCategory { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
