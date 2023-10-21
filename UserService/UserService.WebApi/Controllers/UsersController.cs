using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.AsyncDataServices;
using UserService.Application.Interfaces;
using UserService.Application.ViewModels.Users;

namespace UserService.WebApi.Controllers
{
    public class UsersController:BaseController
    {
        private readonly IMemberService _service;
        private readonly IMessageBusClient _messageBusClient;   
        public UsersController(IMemberService service,IMessageBusClient messageBus)
        {
            _service = service;
            _messageBusClient=messageBus;
        }

        [Authorize]
        [HttpGet("{id}/payments")]
        public async Task<IActionResult> GetPaymentsByUserId(Guid id)
        {
            var result = await _service.GetPaymentsByUserId(id);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await _service.GetUserById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserCreateModel model)
        {
            var result = await _service.CreateUser(model);
            try
            {
                if(result!=null){
                    _messageBusClient.PublishedUser(model);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asyncchronously: {ex.InnerException}");
            }
            return StatusCode(StatusCodes.Status201Created,result);
        }
    }
}
