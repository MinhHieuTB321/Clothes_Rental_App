using ComboService.Application.Interfaces;
using ComboService.Application.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;

namespace ComboService.WebApi.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseModel>>> GetAll()
        {
            var rs = await _service.GetProducts();
            return Ok(rs);
        }
    }
}
