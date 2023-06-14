namespace bill.ViewModel
{
    public class ReceiptDetailViewModel
    {
        public int receipt_detail_id { get; set; }

        public int quantity { get; set; }

        public int item_id { get; set; }

        public int receipt_id { get; set; }

        public float total_item_price { get; set; }

    }
}
