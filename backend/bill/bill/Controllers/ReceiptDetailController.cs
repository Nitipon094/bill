using bill.Repository;
using bill.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace bill.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReceiptDetailController : ControllerBase
    {
        readonly ReceiptDetailRepository receiptDetailRepository;
        public ReceiptDetailController(ReceiptDetailRepository receiptDetailRepository)
        {
            this.receiptDetailRepository = receiptDetailRepository;
        }

        [HttpGet]
        public IActionResult Getall()
        {
            var result = receiptDetailRepository.Getall();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddReceiptDetail(ReceiptDetailViewModel d)
        {
            receiptDetailRepository.AddReceiptDetail(d.receipt_id, d.item_id, d.quantity, d.total_item_price);
            return Ok(1);
        }
    }
}
