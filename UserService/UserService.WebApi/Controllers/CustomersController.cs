using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Interfaces;
using UserService.Domain.Enums;

namespace UserService.WebApi.Controllers
{
    public class CustomersController:BaseController
    {
        private readonly IMemberService _service;

        public CustomersController(IMemberService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get All Customer
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            var result = await _service.GetAllUserByRole(RoleEnums.Customer.ToString());
            return Ok(result);
        }
        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var result = await _service.GetCustomerById(id);
            return Ok(result);
        }


        [Authorize]
        [HttpGet("{id}/payments")]
        public async Task<IActionResult> GetPaymentsByUserId(Guid id)
        {
            var result = await _service.GetPaymentsByCustomerId(id);
            return Ok(result);
        }
    }
}
