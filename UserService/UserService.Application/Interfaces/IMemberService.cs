using System.Collections.Generic;
using UserService.Application.ViewModels.Payments;
using UserService.Application.ViewModels.Users;

namespace UserService.Application.Interfaces
{
    public interface IMemberService
    {
        Task<List<UserReadModel>> GetAllUserByRole(string role);
        Task<UserReadModel> GetCustomerById(Guid id);
        Task<UserReadModel> GetOwnerById(Guid id);
        Task<List<PaymentReadModel>> GetPaymentsByCustomerId(Guid id);
        Task<List<PaymentReadModel>> GetPaymentsByOwnerId(Guid id);
    }
}