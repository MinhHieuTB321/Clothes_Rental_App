
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Interfaces;
using UserService.Application.ViewModels.Authentications;

namespace UserService.WebApi.Controllers
{
    public class AuthenticationsController:BaseController
    {
        private readonly IAuthenticationService _service;
        public AuthenticationsController(IAuthenticationService service)
        {
            _service = service;
        }


        /// <summary>
        /// Login by email. Use to test
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> LoginAsync(AuthenticationRequestModel model)
        {
            var token = await _service.LoginAsync(model);
            return Ok(token);
        }
    }
}
