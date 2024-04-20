using System;
using System.Collections.Generic;

namespace Forcast.Data.Entities;

public partial class MaterialMaster
{
    public int Id { get; set; }

    public int Material { get; set; }

    public string DpName { get; set; } = null!;

    public string Description { get; set; } = null!;
}
