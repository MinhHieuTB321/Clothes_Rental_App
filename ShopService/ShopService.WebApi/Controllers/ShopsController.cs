using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopService.Application.Interfaces;
using ShopService.Application.ViewModels.Shops;
using ShopService.Domain.Enum;
using System.Net.WebSockets;

namespace ShopService.WebApi.Controllers
{
    public class ShopsController : BaseController
    {
        private readonly IShopService _service;
        private readonly IMessageBusClient _messageBusClient;
        public ShopsController(IShopService service, IMessageBusClient messageBusClient)
        {
            _service = service;
            _messageBusClient = messageBusClient;
        }

        // public ShopsController(IShopService service)
        // {
        //     _service = service;
        // }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllShop()
        {
            var result = await _service.GetAllAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShopById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetAllProductByShopId(Guid id, int pageNumber = 0, int pageSize = 10)
        {
            var result = await _service.GetAllProductByShopId(id, pageNumber,pageSize);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [Authorize(Roles =("Admin,Owner"))]
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteShop(Guid id)
        {
            var result = await _service.DeleteShop(id);
            if(!result) return BadRequest();
            try
            {
                if (result)
                {
                    _messageBusClient.UpdatedShop(new ShopReadModel{Id=id});
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asyncchronously: {ex.InnerException}");
            }
            return NoContent();
        }

        [Authorize(Roles = ("Admin,Owner"))]
        [HttpPost]
        public async Task<IActionResult> CreateShop([FromForm] ShopCreateModel model)
        {
            var result= await _service.CreateShop(model);
            if(result is not null)
            {
                try
                {
                    if (result != null)
                    {
                        _messageBusClient.PublishedNewShop(result);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not send asyncchronously: {ex.InnerException}");
                }
                return CreatedAtAction(nameof(GetShopById), new { id = result!.Id }, result);
            }
            return BadRequest();
        }

        [Authorize(Roles = ("Admin,Owner"))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShop(Guid id,[FromForm] ShopUpdateModel model)
        {
            if(id!=model.Id) return BadRequest();
            var result= await _service.UpdateShop(model);
            if(result is not null)
            {
                try
                {
                    if (result != null)
                    {
                        _messageBusClient.UpdatedShop(result);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not send asyncchronously: {ex.InnerException}");
                }
                 return NoContent();
            }
            return BadRequest();
        }
    }
}
