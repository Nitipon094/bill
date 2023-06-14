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

        public int AddReceipt(string code, DateTime date, float? total_price, float net_price, float vat, float pre_vat, float discount)
        {
            receipt newReceipt = new receipt{ code = code,
                                             date = date,
                                             total_price = total_price,
                                             net_price = net_price,
                                             vat = vat,
                                             pre_vat = pre_vat,
                                             discount = discount
                                            };
            billDbContext.receipts.Add(newReceipt);
            billDbContext.SaveChanges();

            int receipt_id = billDbContext.receipts
                            .OrderByDescending(r => r.receipt_id)
                            .Select(r => r.receipt_id)
                            .FirstOrDefault();

            return receipt_id;
        }
    }
}
