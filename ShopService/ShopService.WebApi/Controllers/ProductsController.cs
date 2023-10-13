using Microsoft.AspNetCore.Mvc;
using ShopService.Application.Interfaces;
using ShopService.Application.Services;
using ShopService.Application.ViewModels.Products;
using ShopService.Domain.Entities;

namespace ShopService.WebApi.Controllers
{
    public class ProductsController:BaseController
    {
        private readonly IProductService _service;
        private readonly IProductImageService _imageService;

        public ProductsController(IProductService service, IProductImageService imageService)
        {
            _service = service;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var result= await _service.GetAllAsync();
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return BadRequest();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult>CreateProduct([FromForm]ProductCreateModel model)
        {
            var result = await _service.CreateProduct(model);
            if (result is not null)
            {
                var image = new List<ProductImage>();
                foreach (var item in model.File)
                {
                    var createdImage = await _imageService.AddImageAsync(item, result.Id);
                    if (createdImage is not null)
                    {
                        result.ProductImages.Add(createdImage);
                    }
                    else throw new Exception("Create Image failed!");
                }
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            else return BadRequest(); 
        }
        [HttpPut]
        public async Task<IActionResult>UpdateProduct(ProductUpdateModel model)
        {
            var result = await _service.UpdateProduct(model);
            if (result is not null) return NoContent();
            else return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteProduct(Guid id)
        {
            var result = await _service.DeleteProduct(id);
            if (result) return Ok();
            return BadRequest();
        }
    }
}
