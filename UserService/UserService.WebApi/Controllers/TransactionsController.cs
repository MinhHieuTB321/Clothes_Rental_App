using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Interfaces;
using UserService.Application.ViewModels.Transactions;

namespace UserService.WebApi.Controllers
{
    public class TransactionsController:BaseController
    {
        private readonly ITransactionService _service;
        public TransactionsController(ITransactionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create Transaction      
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTransaction(TransactionCreateModel model)
        {
            var result= await _service.CreateTransaction(model);
            return CreatedAtAction(nameof(GetTransactionById),new {id=result.Id},result);
        }
        /// <summary>
        /// Get All Transaction
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllTransaction(Guid paymentId)
        {
            var result= await _service.GetAllTransactions(paymentId);
            return Ok(result);
        }
        /// <summary>
        /// Get Transaction by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(Guid id,[FromQuery] Guid paymentId)
        {
            var result = await _service.GetTransactionById(id,paymentId);
            return Ok(result);
        }
    }
}
