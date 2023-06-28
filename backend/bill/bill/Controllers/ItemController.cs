using bill.Repository;
using bill.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace bill.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ItemController : ControllerBase
    {
        readonly ItemRepository itemRepository;
        public ItemController(ItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        [HttpGet]
        public IActionResult Getall()
        {
            var result = itemRepository.Getall();
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetMaxCode()
        {
            var maxCode = "";
            var items = itemRepository.Getall();
            if (items == null || items .Count == 0)
            {
                maxCode = "PD-00000001";
            }
            else
            {
                maxCode = itemRepository.GetMaxCode();
                string prefix = maxCode.Substring(0, maxCode.IndexOf("-") + 1);
                string numberString = maxCode.Substring(maxCode.IndexOf("-") + 1);
                int number = int.Parse(numberString);
                number++;
                maxCode = prefix + number.ToString("D6");
            }
            return Ok(maxCode);
        }

        [HttpPost]
        public IActionResult GetByCode(ItemCodeViewModel i)
        {
            var result = itemRepository.GetByCode(i.code);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddItem(ItemAddViewModel i) 
        {
            itemRepository.AddItem(i.code, i.name, i.price, i.unit_id);
            return Ok(1);
        }

        [HttpPost]
        public IActionResult UpdateItem(ItemUpdateViewModel i)
        {
            itemRepository.UpdateItem(i.item_id, i.name, i.price, i.unit_id);
            return Ok(1);
        }

        [HttpPost]
        public IActionResult DeleteItem(ItemDeleteViewModel i)
        {
            itemRepository.DeleteItem(i.item_id);
            return Ok(1);
        }
    }
}
