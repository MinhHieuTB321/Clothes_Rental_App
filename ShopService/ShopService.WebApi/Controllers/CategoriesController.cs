using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopService.Application.Interfaces;
using ShopService.Application.ViewModels.Categories;
using ShopService.Domain.Enum;
using System.Runtime.CompilerServices;

namespace ShopService.WebApi.Controllers
{
    public class CategoriesController:BaseController
    {
        private readonly ICategoryService _service;
        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await _service.GetAllAsync();
            if (result == null) return BadRequest();
            return Ok(result);
        }


        [Authorize(Roles = nameof(RoleEnum.Admin))]
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteCategory(Guid id)
        {
            var result = await _service.DeleteCategory(id);
            if(!result) return BadRequest();
            return NoContent();  
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult>GetCategoryById(Guid id)
        {
            var result= await _service.GetByIdAsync(id);
            if(result == null) return BadRequest();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}/products")]
        public async Task<IActionResult>GetAllProductByCategoryId(Guid id)
        {
            var result= await _service.GetAllProductByCateId(id);
            if(result == null) return BadRequest();
            return Ok(result);
        }

        [Authorize(Roles = nameof(RoleEnum.Admin))]
        [HttpPost]
        public async Task<IActionResult>CreateCategory([FromForm]CategoryCreateModel model)
        {
            Console.WriteLine(model.CategoryName);
            var result = await _service.CreateCategory(model);
            if(result == null) return BadRequest();
            return CreatedAtAction(nameof(GetCategoryById), new {id=result.Id},result);
        }

        [Authorize(Roles = nameof(RoleEnum.Admin))]
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateCategory(Guid id, [FromForm]CategoryUpdateModel model)
        {
            Console.WriteLine(model.Id);
            Console.WriteLine(model.CategoryName);
            if (id != model.Id) return BadRequest();
            var result=await _service.UpdateCategory(model);
            if(result == null) return BadRequest();
            return NoContent();
        }
    }
}
