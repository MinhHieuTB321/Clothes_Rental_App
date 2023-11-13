using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.AsyncDataServices;
using OrderService.Application.Interfaces;
using OrderService.Application.ViewModels.Orders;

namespace OrderService.WebApi.Controllers
{
    public class OrdersController:BaseController
    {
        private readonly IOrderService _service;
        private readonly IMessageBusClient _messageBusClient;
        public OrdersController(IOrderService service,IMessageBusClient messageBusClient)
        {
            _service = service;
            _messageBusClient = messageBusClient;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrder(Guid? shopId)
        {
            if(shopId!=null)return Ok(await _service.GetAllOrderyShopId(shopId.Value));
            var result = await _service.GetAllOrders();
            return Ok(result);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetOrderById(id);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}/orders-detail")]
        public async Task<IActionResult> GetOrderDetailById(Guid id)
        {
            var result = await _service.GetOrderDetailByOrderId(id);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id,OrderUpdateModel model)
        {
            if(id!=model.Id)
                return BadRequest("Id is not match!");
            var result = await _service.UpdateOrder(model);
            return NoContent();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateModel model)
        {
            var result= await _service.CreateOrder(model);

            result = await _service.GetOrderById(result.Id);
            try
            {
                _messageBusClient.PublishNewOrder(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asyncchronously: {ex.InnerException}");
            }

            return CreatedAtAction(nameof(GetById),new {id=result.Id},result);
        }


        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id){
             var result= await _service.DeleteOrder(id);
            try
            {
                _messageBusClient.DeleteOrder(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asyncchronously: {ex.InnerException}");
            }

            return CreatedAtAction(nameof(GetById),new {id=result.Id},result);
        }
    }
}
