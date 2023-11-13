using ComboService.Application.Interfaces;
using ComboService.Application.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;

namespace ComboService.WebApi.Controllers
{
    public class ShopController : BaseController
    {
        private readonly IShopService _service;

        public ShopController(IShopService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all shops
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShopResponseModel>>> GetAll()
        {
            var rs = await _service.GetShops();
            return Ok(rs);
        }
    }
}
