using UserService.Application.ViewModels.Orders;
using UserService.Application.ViewModels.Users;

namespace UserService.Application.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishedUpdateOrder(OrderUpdatePublishedModel model);
        void PublishedUser(UserReadModel model);
		void UpdatedUser(UserReadModel model);
		void DeletedUser(UserReadModel model);
	}
}
