using bill.Context;
using bill.Models;
using bill.ViewModel;
using Microsoft.OpenApi.Any;

namespace bill.Repository
{
    public class ItemRepository
    {
        readonly BillDbContext billDbContext;
        public ItemRepository (BillDbContext billDbContext)
        {
            this.billDbContext = billDbContext;
        }

        public List<ItemViewModel> Getall()
        {
            List<ItemViewModel> result = (from item in billDbContext.items
                                              join unit in billDbContext.units on item.unit_id equals unit.unit_id
                                              orderby item.item_id ascending
                                              select new ItemViewModel
                                              {
                                                  item_id = item.item_id,
                                                  code = item.code,
                                                  name = item.name,
                                                  price = (float)item.price,
                                                  unit_id = unit.unit_id,
                                                  unit_name = unit.name
                                              }).ToList();
            return result;
        }
        public void AddItem(string code, string name, float price, int unit_id)
        {
            item newItem = new item { name = name,
                                      code = code,
                                      price = (decimal?)price,
                                      unit_id = unit_id
                                    };

            billDbContext.items.Add(newItem);
            billDbContext.SaveChanges();
        }
    }
}
