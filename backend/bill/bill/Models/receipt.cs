using System;
using System.Collections.Generic;

namespace bill.Models;

public partial class receipt
{
    public int receipt_id { get; set; }

    public string? code { get; set; }

    public DateTime date { get; set; }

    public float? total_price { get; set; }

    public float vat { get; set; }

    public float pre_vat { get; set; }

    public float discount { get; set; }

    public float net_price { get; set; }

    public virtual ICollection<receipt_detail> receipt_details { get; set; } = new List<receipt_detail>();
}
