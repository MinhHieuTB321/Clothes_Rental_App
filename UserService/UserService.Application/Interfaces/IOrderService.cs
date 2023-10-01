using UserService.Application.ViewModels.Payments;

namespace UserService.Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<PaymentReadModel>> GetAllPaymentsByOrderId(Guid orderId);
    }   
}