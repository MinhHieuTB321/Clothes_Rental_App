using Microsoft.AspNetCore.Mvc;
using ShopService.Application.Interfaces;

namespace ShopService.WebApi.Controllers
{
    public class ShopsController:BaseController
    {
        private readonly IShopService _service;
        public ShopsController(IShopService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllShop()
        {
            var result= await _service.GetAllAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }
    }
}
