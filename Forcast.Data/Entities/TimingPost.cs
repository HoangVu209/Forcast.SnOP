using System;
using System.Collections.Generic;

namespace Forcast.Data.Entities;

public partial class TimingPost
{
    public int Id { get; set; }

    public string Customer { get; set; } = null!;

    public string PostName { get; set; } = null!;

    public DateOnly PostStart { get; set; }

    public DateOnly PostEnd { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public virtual UserAssign CreatedByNavigation { get; set; } = null!;
}
