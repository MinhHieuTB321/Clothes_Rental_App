using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Interfaces;

namespace OrderService.WebApi.Controllers
{
    public class ShopsController:BaseController
    {
        private readonly IShopService _shopService;
        public ShopsController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [Authorize(Roles ="Owner")]
        [HttpGet("{id}/orders")]
        public async Task<IActionResult> GetOrderByShopId(Guid id)
        {
            var result = await _shopService.GetAllOrderyShopId(id);
            return Ok(result);
        }
    }
}
