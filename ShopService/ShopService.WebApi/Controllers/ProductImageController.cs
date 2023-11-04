using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopService.Application.Interfaces;
using ShopService.Application.ViewModels.Images;
using ShopService.Domain.Enum;

namespace ShopService.WebApi.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _service;

        public ProductImageController(IProductImageService service)
        {
            _service=service;
        }

        [Authorize(Roles = nameof(RoleEnum.Owner))]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ImageCreateModel model){
            var result = await _service.AddImageAsync(model);
            return StatusCode(StatusCodes.Status201Created,result);
        }

        [Authorize(Roles = nameof(RoleEnum.Owner))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id){
            var result = await _service.DeleteImage(id);
            return NoContent();
        }
    }
}