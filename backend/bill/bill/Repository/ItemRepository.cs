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
        public string GetMaxCode() 
        {
            var maxCode = billDbContext.items.Max(item => item.code);

            return maxCode;
        }
        public List<ItemViewModel> GetByCode(string code)
        {
            List<ItemViewModel> result = (from item in billDbContext.items
                                            join unit in billDbContext.units on item.unit_id equals unit.unit_id
                                            where item.code == code
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
                                      price = price,
                                      unit_id = unit_id
                                    };

            billDbContext.items.Add(newItem);
            billDbContext.SaveChanges();
        }
        public void UpdateItem(int item_id, string? name, float price, int? unit_id)
        {
            var itemToEdit = billDbContext.items.FirstOrDefault(i => i.item_id == item_id);

            if (itemToEdit != null)
            {
                itemToEdit.name = name;
                itemToEdit.price = price;
                itemToEdit.unit_id = (int)unit_id;

                billDbContext.SaveChanges();
            }
        }

        public void DeleteItem(int item_id)
        {
            var delItem = billDbContext.items.Where(i => i.item_id == item_id);

            billDbContext.items.RemoveRange(delItem);
            billDbContext.SaveChanges();
        }
    }
}
