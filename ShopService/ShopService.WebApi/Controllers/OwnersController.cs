using Microsoft.AspNetCore.Mvc;
using ShopService.Application.Interfaces;
using ShopService.Application.ViewModels.Owners;

namespace ShopService.WebApi.Controllers
{
    public class OwnersController : BaseController
    {
        private readonly IOwnerService _service;
        public OwnersController(IOwnerService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOwner()
        {
            var result = await _service.GetAllAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOwnerById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result is null) return BadRequest();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOwner([FromForm] OwnerCreateModel ownerCreateModel)
        {
            var result = await _service.CreteOwner(ownerCreateModel);
            if(result is null) return BadRequest(); 
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteOwner(Guid id)
        {
            var result = await _service.DeleteOwner(id);
            if(result) return Ok();
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult>UpdateOwner([FromForm]OwnerUpdateModel ownerUpdateModel)
        {
            var result= await _service.UpdateOwner(ownerUpdateModel);
            if(result is null) return BadRequest();
            return Ok(result);
        }
    }
}
