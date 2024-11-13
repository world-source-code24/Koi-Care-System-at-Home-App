using System;
using System.Collections.Generic;

namespace Business_Object;

public partial class ShopsTbl
{
    public int ShopId { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<ProductsTbl> ProductsTbls { get; set; } = new List<ProductsTbl>();
}
