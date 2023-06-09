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
        [HttpPost]
        public IActionResult AddItem(ItemViewModel i) 
        {
            itemRepository.AddItem(i.code, i.name, i.price, i.unit_id);
            return Ok(1);
        }
    }
}
