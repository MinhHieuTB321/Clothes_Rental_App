using UserService.Application.ViewModels.Orders;
using UserService.Application.ViewModels.Payments;
using UserService.Application.ViewModels.Transactions;
using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentReadModel> CreatePayment(PaymentCreateModel createModel);

        Task<PaymentReadModel> GetPaymentById(Guid id);

        Task<List<PaymentReadModel>> GetAllPayments();
    }
}