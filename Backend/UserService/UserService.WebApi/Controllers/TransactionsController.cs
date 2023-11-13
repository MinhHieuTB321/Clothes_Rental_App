using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.AsyncDataServices;
using UserService.Application.Interfaces;
using UserService.Application.ViewModels.Orders;
using UserService.Application.ViewModels.Transactions;

namespace UserService.WebApi.Controllers
{
    public class TransactionsController:BaseController
    {
        private readonly ITransactionService _service;
         private readonly IMessageBusClient _messageBusClient;
        public TransactionsController(ITransactionService service,IMessageBusClient messageBusClient)
        {
            _service = service;
            _messageBusClient=messageBusClient;
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
            try
            {
                if(result!=null){
                    _messageBusClient.PublishedUpdateOrder(new OrderUpdatePublishedModel{Id=model.OrderId});
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asyncchronously: {ex.InnerException}");
            }
            return CreatedAtAction(nameof(GetTransactionById),new {id=result!.Id},result);
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
        /// <param name="paymentId"></param>
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
