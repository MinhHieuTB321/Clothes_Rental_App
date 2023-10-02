using ShopService.Application.Interfaces;

namespace ShopService.WebApi.Controllers
{
    public class ProductsController:BaseController
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }
    }
}
