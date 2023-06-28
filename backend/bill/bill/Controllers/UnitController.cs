using bill.Repository;
using bill.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace bill.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UnitController : ControllerBase
    {
        readonly UnitRepository unitRepository;
        public UnitController(UnitRepository unitRepository)
        {
            this.unitRepository = unitRepository;
        }

        [HttpGet]
        public IActionResult Getall() 
        {
            var result = unitRepository.Getall();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddUnit(UnitViewModel u)
        { 
            unitRepository.AddUnit(u.name);
            return Ok(u.name);
        }

        [HttpPost]
        public IActionResult UpdateUnit(UnitViewModel u)
        {
            unitRepository.UpdateUnit(u.unit_id,u.name);
            return Ok(u.name);
        }

        [HttpPost]
        public IActionResult DeleteUnit(UnitViewModel u)
        {
            unitRepository.DeleteUnit(u.unit_id);
            return Ok(u.name);
        }

    }
}