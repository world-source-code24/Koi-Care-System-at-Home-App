using System;
using System.Collections.Generic;

namespace Business_Object;

public partial class KoisTbl
{
    public int KoiId { get; set; }

    public string Name { get; set; } = null!;

    public string? Image { get; set; }

    public string? Physique { get; set; }

    public int Age { get; set; }

    public decimal? Length { get; set; }

    public decimal? Weight { get; set; }

    public bool? Sex { get; set; }

    public string? Breed { get; set; }

    public int? PondId { get; set; }

    public virtual PondsTbl? Pond { get; set; }
}
