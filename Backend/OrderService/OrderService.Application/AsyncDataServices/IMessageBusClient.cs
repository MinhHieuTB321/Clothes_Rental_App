using OrderService.Application.ViewModels.Customers;
using OrderService.Application.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewOrder(OrderReadModel model);

        void UpdateOrder(OrderReadModel model);

        void DeleteOrder(OrderReadModel model);
    }
}
