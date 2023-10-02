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
    }
}
