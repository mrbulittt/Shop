using System;
using System.Collections.Generic;

namespace Shop.Data;

public partial class Order
{
    public int IdOrder { get; set; }

    public int? IdUser { get; set; }

    public string? Status { get; set; }

    public int? TotalAmount { get; set; }

    public int? IdProduct { get; set; }

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();

    public virtual User? IdUserNavigation { get; set; }
}
