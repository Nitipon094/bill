using System;
using System.Collections.Generic;

namespace bill.Models;

public partial class receipt
{
    public int receipt_id { get; set; }

    public string? code { get; set; }

    public DateTime date { get; set; }

    public decimal? total_price { get; set; }

    public decimal? vat { get; set; }

    public decimal? pre_vat { get; set; }

    public decimal? discount { get; set; }

    public decimal? net_price { get; set; }

    public virtual ICollection<receipt_detail> receipt_details { get; set; } = new List<receipt_detail>();
}
