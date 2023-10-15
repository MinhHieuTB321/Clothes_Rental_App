using OrderService.Application.ViewModels.OrderDetails;
using OrderService.Application.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderReadModel> CreateOrder(OrderCreateModel model);
        Task<OrderReadModel> GetOrderById(Guid id);
        Task<List<OrderReadModel>> GetAllOrders();
        Task<bool> UpdateOrder(OrderUpdateModel model);
        Task<bool> DeleteOrder(Guid id);
        Task<List<OrderDetailReadModel>> GetOrderDetailByOrderId(Guid id);
    }
}
