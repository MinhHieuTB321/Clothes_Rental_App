using ShopService.Application.Interfaces;

namespace ShopService.WebApi.Controllers
{
    public class OwnersController : BaseController
    {
        private readonly IOwnerService _service;
        public OwnersController(IOwnerService service)
        {
            _service = service;
        }
    }
}
