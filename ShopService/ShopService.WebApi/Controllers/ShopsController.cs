using Microsoft.AspNetCore.Mvc;
using ShopService.Application.Interfaces;
using ShopService.Application.ViewModels.Shops;
using System.Net.WebSockets;

namespace ShopService.WebApi.Controllers
{
    public class ShopsController : BaseController
    {
        private readonly IShopService _service;
        public ShopsController(IShopService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllShop()
        {
            var result = await _service.GetAllAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShopById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result is null) return BadRequest();
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteShop(Guid Id)
        {
            var result = await _service.DeleteShop(Id);
            if(!result) return BadRequest();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShop([FromForm] ShopCreateModel model)
        {
            var result= await _service.CreateShop(model);
            if(result is not null) return Ok(result);
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateShop([FromForm] ShopUpdateModel model)
        {
            var result= await _service.UpdateShop(model);
            if(result is not null) return Ok( result);
            return BadRequest();
        }
    }
}
