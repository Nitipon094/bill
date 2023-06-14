using bill.Repository;
using bill.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace bill.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReceiptController : ControllerBase
    {
        readonly ReceiptRepository ReceiptRepository;
        public ReceiptController(ReceiptRepository ReceiptRepository)
        {
            this.ReceiptRepository = ReceiptRepository;
        }

        [HttpGet]
        public IActionResult Getall()
        {
            var result = ReceiptRepository.Getall();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddReceipt(ReceiptViewModel r)
        {
            var result = ReceiptRepository.AddReceipt(r.code, r.date, r.total_price, r.net_price, r.vat, r.pre_vat, r.discount);
            return Ok(result);
        }
    }
}
