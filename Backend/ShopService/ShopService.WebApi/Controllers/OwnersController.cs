using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopService.Application.Interfaces;
using ShopService.Application.ViewModels.Owners;
using ShopService.Domain.Enum;

namespace ShopService.WebApi.Controllers
{
    public class OwnersController : BaseController
    {
        private readonly IOwnerService _service;
        public OwnersController(IOwnerService service)
        {
            _service = service;
        }

        [Authorize(Roles = nameof(RoleEnum.Admin))]
        [HttpGet]
        public async Task<IActionResult> GetAllOwner()
        {
            var result = await _service.GetAllAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [Authorize(Roles = "Owner,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOwnerById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}/shops")]
        public async Task<IActionResult> GetShopByOwnerId(Guid id)
        {
            var result = await _service.GetAllShopByOwnerId(id);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOwner([FromForm] OwnerCreateModel ownerCreateModel)
        {
            var result = await _service.CreteOwner(ownerCreateModel);
            if(result is null) return BadRequest();
            return CreatedAtAction(nameof(GetOwnerById), new { id = result!.Id }, result);

        }

        [Authorize(Roles = nameof(RoleEnum.Admin))]
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteOwner(Guid id)
        {
            var result = await _service.DeleteOwner(id);
            if(result) return NoContent();
            return BadRequest();
        }

        [Authorize(Roles = "Admin,Owner")]
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateOwner(Guid id, [FromForm]OwnerUpdateModel ownerUpdateModel)
        {
            if (id != ownerUpdateModel.Id) return BadRequest();
            var result= await _service.UpdateOwner(ownerUpdateModel);
            if(result is null) return BadRequest();
            return NoContent();
        }
    }
}
