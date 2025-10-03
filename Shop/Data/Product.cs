using System;
using System.Collections.Generic;

namespace Shop.Data;

public partial class Product
{
    public int IdProduct { get; set; }

    public string? NameProduct { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? IdCategory { get; set; }

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();

    public virtual ProdCategory? IdCategoryNavigation { get; set; }
}
