using System;
using System.Collections.Generic;

namespace Business_Object;

public partial class AccountTbl
{
    public int AccId { get; set; }

    public string? Name { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Image { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string Role { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<CartTbl> CartTbls { get; set; } = new List<CartTbl>();

    public virtual ICollection<OrdersTbl> OrdersTbls { get; set; } = new List<OrdersTbl>();

    public virtual ICollection<PondsTbl> PondsTbls { get; set; } = new List<PondsTbl>();
}
