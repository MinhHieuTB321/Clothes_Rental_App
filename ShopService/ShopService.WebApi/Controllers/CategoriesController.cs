using Microsoft.AspNetCore.Mvc;
using ShopService.Application.Interfaces;
using ShopService.Application.ViewModels.Categories;
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
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await _service.GetAllAsync();
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult>DeleteCategory(Guid id)
        {
            var result = await _service.DeleteCategory(id);
            if(result) return BadRequest();
            return Ok(result);  
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetCategoryById(Guid Id)
        {
            var result= await _service.GetByIdAsync(Id);
            if(result == null) return BadRequest();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult>CreateCategory([FromForm]CategoryCreateModel model)
        {
            var result = await _service.CreateCategory(model);
            if(result == null) return BadRequest();
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult>UpdateCategory([FromForm]CategoryUpdateModel model)
        {
            var result=await _service.UpdateCategory(model);
            if(result == null) return BadRequest();
            return Ok(result);
        }
    }
}
