using System;
using System.Collections.Generic;

namespace Shop.Data;

public partial class Login
{
    public int IdLogin { get; set; }

    public string? NameLogin { get; set; }

    public string? Password { get; set; }

    public int? IdUser { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
