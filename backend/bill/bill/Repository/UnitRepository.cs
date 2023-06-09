using bill.Context;
using bill.Models;
using bill.ViewModel;
using Microsoft.OpenApi.Any;

namespace bill.Repository
{
    public class UnitRepository
    {
        readonly BillDbContext billDbContext;
        public UnitRepository (BillDbContext billDbContext)
        {
            this.billDbContext = billDbContext;
        }
        public List<UnitViewModel> Getall()
        {
            List<UnitViewModel> result = (from a in billDbContext.units
                                    orderby a.unit_id ascending
                                    select new UnitViewModel
                                    {
                                        unit_id = a.unit_id,
                                        name = a.name
                                    }).ToList();
            return result;
        }

        public void AddUnit(string name)
        {
            unit newUnit = new unit { name = name };

            billDbContext.units.Add(newUnit);
            billDbContext.SaveChanges();
        }

        public void UpdateUnit(int unit_id, string name)
        {
            var unitToEdit = billDbContext.units.FirstOrDefault(u => u.unit_id == unit_id);

            if (unitToEdit != null)
            {
                unitToEdit.name = name;

                billDbContext.SaveChanges();
            }
        }

        public void DeleteUnit(int unit_id)
        {
            var delUnit = billDbContext.units.Where(u => u.unit_id == unit_id);

            billDbContext.units.RemoveRange(delUnit);
            billDbContext.SaveChanges();
        }

    }
}
