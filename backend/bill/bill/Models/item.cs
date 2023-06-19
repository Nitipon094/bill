using System;
using System.Collections.Generic;

namespace bill.Models;

public partial class item
{
    public int item_id { get; set; }

    public string? code { get; set; }

    public string? name { get; set; }

    public float price { get; set; }

    public int unit_id { get; set; }

    public virtual ICollection<receipt_detail> receipt_details { get; set; } = new List<receipt_detail>();

    public virtual unit unit { get; set; } = null!;
}
