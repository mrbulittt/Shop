using System;
using System.Collections.Generic;

namespace Shop.Data;

public partial class Basket
{
    public int IdBasket { get; set; }

    public int IdProduct { get; set; }

    public int IdUser { get; set; }

    public int? ProdCount { get; set; }

    public decimal? ResultPrice { get; set; }

    public bool? IsOrder { get; set; }

    public int? IdOrder { get; set; }

    public virtual Order? IdOrderNavigation { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
