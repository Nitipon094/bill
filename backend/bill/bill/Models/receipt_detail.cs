using System;
using System.Collections.Generic;

namespace bill.Models;

public partial class receipt_detail
{
    public int receipt_detail_id { get; set; }

    public int quantity { get; set; }

    public int item_id { get; set; }

    public int receipt_id { get; set; }

    public float total_item_price { get; set; }

    public virtual item item { get; set; } = null!;

    public virtual receipt receipt { get; set; } = null!;
}
