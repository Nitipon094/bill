using System;
using System.Collections.Generic;

namespace bill.Models;

public partial class unit
{
    public int unit_id { get; set; }

    public string? name { get; set; }

    public virtual ICollection<item> items { get; set; } = new List<item>();
}
