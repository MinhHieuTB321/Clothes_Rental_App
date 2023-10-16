using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Interfaces;

namespace UserService.WebApi.Controllers
{
    public class UsersController:BaseController
    {
        private readonly IMemberService _service;

        public UsersController(IMemberService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet("{id}/payments")]
        public async Task<IActionResult> GetPaymentsByUserId(Guid id)
        {
            var result = await _service.GetPaymentsByUserId(id);
            return Ok(result);
        }
    }
}
