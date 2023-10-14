using UserService.Application.ViewModels.Orders;

namespace UserService.Application.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishedUpdateOrder(OrderUpdatePublishedModel model);
    }
}
