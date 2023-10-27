using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.AsyncDataServices;
using OrderService.Application.Interfaces;
using OrderService.Application.ViewModels.Customers;
using OrderService.Domain.Enums;

namespace OrderService.WebApi.Controllers
{
    public class CustomersController:BaseController
    {
        private readonly ICustomerService _service;
        private readonly IMessageBusClient _messageBusClient;
        public CustomersController(ICustomerService service,IMessageBusClient messageBusClient)
        {
            _service = service;
            _messageBusClient = messageBusClient;
        }

        /// <summary>
        /// Get all customer
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles =nameof(RoleEnum.Owner))]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result= await _service.GetAllCustomers();
            return Ok(result);
        }

        /// <summary>
        /// Get customer by id
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _service.GetCustomerById(id);
            return Ok(result);
        }


        /// <summary>
        /// Get order of customer
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}/orders")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var result = await _service.GetAllOrderByCustomerID(id);
            return Ok(result);
        }

        /// <summary>
        /// Create Customer
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CustomerCreateModel model)
        {
            var result = await _service.CreateCustomer(model);
            return CreatedAtAction(nameof(Get),new {id=result.Id},result);
        }

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = nameof(RoleEnum.Owner))]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id,CustomerUpdateModel model)
        {
            if (id != model.Id) return BadRequest($"Id-{id} is not match with model-{model.Id}!");
            var result = await _service.UpdateCustomer(model);
            return NoContent();
        }

        /// <summary>
        ///Delete Customer by id
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles =nameof(RoleEnum.Admin))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteCustomer(id);
            return NoContent();
        }
    }
}
