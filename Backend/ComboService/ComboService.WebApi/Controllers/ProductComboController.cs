using ComboService.Application.Interfaces;
using ComboService.Application.ViewModels.Request;
using ComboService.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComboService.WebApi.Controllers
{
	[Route("api/products-combo")]
	[ApiController]
	public class ProductComboController:ControllerBase
	{
		private readonly IProductComboService _service;

		public ProductComboController(IProductComboService service)
		{
			_service = service;
		}

		/// <summary>
		/// Create products-combo
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[Authorize(Roles = nameof(RoleEnum.Owner))]
		[HttpPost]
		public async Task<IActionResult> Create(List<ProductComboRequestModel> request)
		{
			var result = await _service.Create(request);
			return StatusCode(StatusCodes.Status201Created, result);

		}

		[Authorize(Roles = nameof(RoleEnum.Owner))]
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductComboRequest model)
		{
			if(model.Id !=id) return BadRequest($"ID is not match with {id}!");
			var result = await _service.Update(model);
			return NoContent();
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var result = await _service.GetProductComboById(id);
			return Ok(result);
		}
		
		[Authorize(Roles = nameof(RoleEnum.Owner))]
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _service.Delete(id);
			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllByComboId(Guid comboId,Guid shopId=default)
		{
            if (shopId != Guid.Empty)
            {
				var nonProductCombo=await _service.GetNonProductByComboId(comboId,shopId);
				return Ok(nonProductCombo);
			}
			var result = await _service.GetAllByComboId(comboId);
			return Ok(result);
		}
	}
}
