using System;
using System.Collections.Generic;

namespace Shop.Data;

public partial class User
{
    public int IdUser { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNum { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public int? IdRole { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();

    public virtual Role? IdRoleNavigation { get; set; }
}
