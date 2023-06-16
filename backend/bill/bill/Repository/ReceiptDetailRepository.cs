using bill.Context;
using bill.Models;
using bill.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

namespace bill.Repository
{
    public class ReceiptDetailRepository
    {
        readonly BillDbContext billDbContext;
        public ReceiptDetailRepository (BillDbContext billDbContext)
        {
            this.billDbContext = billDbContext;
        }
        public List<ReceiptDetailViewModel> Getall()
        {
            List<ReceiptDetailViewModel> result = (from a in billDbContext.receipt_details
                                              orderby a.receipt_detail_id ascending
                                              select new ReceiptDetailViewModel
                                              {
                                                  receipt_detail_id = a.receipt_detail_id,
                                                  quantity = a.quantity,
                                                  item_id = a.item_id,
                                                  receipt_id = a.receipt_id
                                              }).ToList();
            return result;
        }

        public List<ReceiptDetailViewModel> GetById(int id)
        {
            List<ReceiptDetailViewModel> result = (from rd in billDbContext.receipt_details
                                          join i in billDbContext.items on rd.item_id equals i.item_id
                                          join u in billDbContext.units on i.unit_id equals u.unit_id
                                          where rd.receipt_id == id
                                          select new ReceiptDetailViewModel
                                          {
                                              item_code = i.code,
                                              item_name = i.name,
                                              item_price = (float)i.price,
                                              unit_name = u.name,
                                              quantity = rd.quantity,
                                              total_item_price = rd.total_item_price
                                          }).ToList();
            return result;
        }
        public void AddReceiptDetail(int receipt_id, int item_id, int quantity, float total_item_price)
        {
            receipt_detail newReceiptDetail = new receipt_detail { receipt_id = receipt_id,
                                                                   item_id = item_id,
                                                                   quantity = quantity,
                                                                   total_item_price = total_item_price
            };
            billDbContext.receipt_details.Add(newReceiptDetail);
            billDbContext.SaveChanges();
        }

    }
}
