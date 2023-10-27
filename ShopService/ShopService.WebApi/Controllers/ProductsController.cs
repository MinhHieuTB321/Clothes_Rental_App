using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopService.Application.Interfaces;
using ShopService.Application.Services;
using ShopService.Application.ViewModels.Products;
using ShopService.Domain.Entities;
using ShopService.Domain.Enum;

namespace ShopService.WebApi.Controllers
{
    public class ProductsController:BaseController
    {
        private readonly IProductService _service;
        private readonly IMessageBusClient _messageBusClient;
        public ProductsController(IProductService service, IMessageBusClient messageBusClient)
        {
            _service = service;
            _messageBusClient = messageBusClient;
        }

        // public ProductsController(IProductService service)
        // {
        //     _service = service;
        // }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var result= await _service.GetAllAsync();
            if (result == null) return BadRequest();
            return Ok(result);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}/sub-products")]
        public async Task<IActionResult> GetSubProductByRootId(Guid id)
        {
            var result = await _service.GetAllSubProductByRootId(id);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [Authorize(Roles =nameof(RoleEnum.Owner))]
        [HttpPost]
        public async Task<IActionResult>CreateProduct([FromForm]ProductCreateModel model)
        {
            var result = await _service.CreateProduct(model);
            if (result is not null)
            {
                try
                {
                    if (result != null)
                    {
                        _messageBusClient.PublishedNewProduct(await _service.GetByIdAsync(result.Id));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not send asyncchronously: {ex.InnerException}");
                }
                return CreatedAtAction(nameof(GetById), new { id = result!.Id }, result);
            }
            else return BadRequest(); 
        }

        [Authorize(Roles = nameof(RoleEnum.Owner))]
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateProduct(Guid id,[FromForm] ProductUpdateModel model)
        {
            if (id != model.Id) return BadRequest();
            var result = await _service.UpdateProduct(model);
            if (result) 
            {
                try
                {
                    if (result)
                    {
                        _messageBusClient.UpdatedProduct(await _service.GetByIdAsync(id));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not send asyncchronously: {ex.InnerException}");
                }
                return NoContent();
            }
            else return BadRequest();
        }


        [Authorize(Roles = nameof(RoleEnum.Owner))]
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteProduct(Guid id)
        {
            var result = await _service.DeleteProduct(id);
            if (result) 
            {
                try
                {
                    if (result)
                    {
                        var model=new ProductReadModel{Id=id};
                        _messageBusClient.DeletedProduct(model);
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
