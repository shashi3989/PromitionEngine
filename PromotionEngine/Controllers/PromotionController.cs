using Microsoft.AspNetCore.Mvc;
using PromotionEngine.Models;
using PromotionEngine.PromotionEngine.BusinessLayer;


namespace PromotionEngine.Controllers
{
    [Route("api/[controller]")]
    public class PromotionController : Controller
    {
        private readonly IPromotionEngineBusinessRepo _businessRepo;

        public PromotionController(IPromotionEngineBusinessRepo businessRepo)
        {
            _businessRepo = businessRepo;
        }
        
        [HttpPost]
        public  IActionResult CalculateTotalAmout([FromBody] CartItem[] items)
        {

            if (items == null)
            {
                return BadRequest();
            }
            return Ok(new { CartItems = items, TotalValue = _businessRepo.CalculateTotalAmount(items) });
        }
    }
}
