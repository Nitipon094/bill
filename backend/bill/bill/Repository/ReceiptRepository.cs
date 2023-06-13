using bill.Context;
using bill.Models;
using bill.ViewModel;
using Microsoft.OpenApi.Any;

namespace bill.Repository
{
    public class ReceiptRepository
    {
        readonly BillDbContext billDbContext;
        public ReceiptRepository(BillDbContext billDbContext)
        {
            this.billDbContext = billDbContext;
        }

        public List<ReceiptViewModel> Getall()
        {
            List<ReceiptViewModel> result = (from r in billDbContext.receipts
                                          orderby r.receipt_id ascending
                                          select new ReceiptViewModel
                                          {
                                              receipt_id = r.receipt_id,
                                              code = r.code,
                                              date = r.date,
                                              total_price = r.total_price
                                          }).ToList();
            return result;
        }

        public void AddReceipt(string code, string date, float? total_price)
        {
            receipt newRecipt = new receipt{ code = code,
                                             date = date,
                                             total_price = total_price
                                            };
            billDbContext.receipts.Add(newRecipt);
            billDbContext.SaveChanges();
        }
    }
}
