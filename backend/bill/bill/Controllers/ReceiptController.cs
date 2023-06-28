using bill.Repository;
using bill.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult GetallFilterDate(ReceiptFilterViewModel r)
        {
            if (r.startDate == null)
            {
                r.startDate = "2000-01-01";
            }
            if (r.endDate == null)
            {
                r.endDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var result = ReceiptRepository.GetallFilterDate(r.startDate, r.endDate);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetMaxCode() 
        {
            var maxCode = "";
            var receipts = ReceiptRepository.Getall();
            if (receipts == null || receipts.Count == 0)
            {
                maxCode = "REC-00000001";
            }
            else
            {
                maxCode = ReceiptRepository.GetMaxCode();
                string prefix = maxCode.Substring(0, maxCode.IndexOf("-") + 1);
                string numberString = maxCode.Substring(maxCode.IndexOf("-") + 1);
                int number = int.Parse(numberString);
                number++;
                maxCode = prefix + number.ToString("D8");
            }
            return Ok(maxCode);
        }

        [HttpPost]
        public IActionResult GetById(ReceiptIdViewModel r)
        {
            var result = ReceiptRepository.GetById(r.receipt_id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddReceipt(ReceiptAddViewModel r)
        {
            var result = ReceiptRepository.AddReceipt(r.code, r.date, r.total_price, r.net_price, r.vat, r.pre_vat, r.discount);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetMinDateReceipt()
        {
            var result = ReceiptRepository.GetMinDateReceipt();
            return Ok(result);
        }
    }
}
