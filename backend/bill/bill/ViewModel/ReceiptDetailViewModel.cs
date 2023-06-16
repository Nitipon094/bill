namespace bill.ViewModel
{
    public class ReceiptDetailViewModel
    {

        public int receipt_detail_id { get; set; }

        public int quantity { get; set; }

        public int item_id { get; set; }

        public int receipt_id { get; set; }

        public float total_item_price { get; set; }

        public string? item_code { get; set; }
        public string? item_name { get; set; }
        public float? item_price { get; set; }
        public string? unit_name { get; set; }
    }
}
