

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Interfaces;
using UserService.Application.ViewModels.Payments;
using UserService.Domain.Entities;

namespace UserService.WebApi.Controllers
{
    public class PaymentsController : BaseController
    {
        private readonly IPaymentService _service;

        public PaymentsController(IPaymentService service)
        {
            _service=service;
        }

        /// <summary>
        /// Get Payment By id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(Guid id)
        {
            var result= await _service.GetPaymentById(id);
            return Ok(result);
        }

        /// <summary>
        /// Get All Payments
        /// </summary>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllPayment()
        {
            var result = await _service.GetAllPayments();
            return Ok(result);
        }
        /// <summary>
        /// Create Payment
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePayment(PaymentCreateModel createModel)
        {
            var result=await _service.CreatePayment(createModel);
            return CreatedAtAction(nameof(GetPaymentById),new {id=result.Id},result);
        }
    }
}
