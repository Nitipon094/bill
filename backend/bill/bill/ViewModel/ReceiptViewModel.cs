namespace bill.ViewModel
{
    public class ReceiptViewModel
    {
        public int receipt_id { get; set; }

        public string? code { get; set; }

        public string? date { get; set; }

        public float? total_price { get; set; }
    }
}
