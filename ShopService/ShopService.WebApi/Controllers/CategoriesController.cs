using ShopService.Application.Interfaces;

namespace ShopService.WebApi.Controllers
{
    public class CategoriesController:BaseController
    {
        private readonly ICategoryService _service;
        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }
    }
}
