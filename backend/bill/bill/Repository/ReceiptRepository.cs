using bill.Context;
using bill.Models;
using bill.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using System;

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
                                              total_price = (float)r.total_price,
                                              vat = (float)r.vat,
                                              pre_vat = (float)r.pre_vat,
                                              discount = (float)r.discount,
                                              net_price = (float)r.net_price
                                          }).ToList();
            return result;
        }

        public DateOnly GetMinDateReceipt()
        {
            var minDate = billDbContext.receipts.Min(receipt => receipt.date);
            return new DateOnly(minDate.Day, minDate.Month, minDate.Year);
        }


        public List<ReceiptViewModel> GetallFilterDate(string sDate, string eDate)
        {
            DateTime startDate = DateTime.Parse(sDate);
            DateTime endDate = DateTime.Parse(eDate);

            List<ReceiptViewModel> result = (from r in billDbContext.receipts
                                             where r.date.Date >= startDate && r.date.Date <= endDate
                                             orderby r.receipt_id ascending
                                             select new ReceiptViewModel
                                             {
                                                 receipt_id = r.receipt_id,
                                                 code = r.code,
                                                 date = r.date,
                                                 total_price = (float)r.total_price,
                                                 vat = (float)r.vat,
                                                 pre_vat = (float)r.pre_vat,
                                                 discount = (float)r.discount,
                                                 net_price = (float)r.net_price
                                             }).ToList();

            return result;
        }

        public string GetMaxCode()
        {
            var maxCode = billDbContext.receipts.Max(receipt => receipt.code);
            return maxCode;
        }
        public List<ReceiptViewModel> GetById(int id)
        {
            List<ReceiptViewModel> result = (from r in billDbContext.receipts
                                             where r.receipt_id == id
                                             orderby r.receipt_id ascending
                                             select new ReceiptViewModel
                                             {
                                                 receipt_id = r.receipt_id,
                                                 code = r.code,
                                                 date = r.date,
                                                 total_price = (float)r.total_price,
                                                 vat = (float)r.vat,
                                                 pre_vat = (float)r.pre_vat,
                                                 discount = (float)r.discount,
                                                 net_price = (float)r.net_price
                                             }).ToList();
            return result;
        }

        public int AddReceipt(string code, DateTime date, float total_price, float net_price, float vat, float pre_vat, float discount)
        {
            receipt newReceipt = new receipt{ code = code,
                                             date = date,
                                             total_price = (decimal?)total_price,
                                             net_price = (decimal?)net_price,
                                             vat = (decimal?)vat,
                                             pre_vat = (decimal?)pre_vat,
                                             discount = (decimal?)discount
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
