using System.Collections.Generic;
using UserService.Application.ViewModels.Payments;
using UserService.Application.ViewModels.Users;

namespace UserService.Application.Interfaces
{
    public interface IMemberService
    {
        Task<UserReadModel> GetUserById(Guid id);   
        Task<UserReadModel> CreateUser(UserCreateModel model);
        Task<List<PaymentReadModel>> GetPaymentsByCustomerId(Guid id);
        Task<List<PaymentReadModel>> GetPaymentsByOwnerId(Guid id);
        Task<List<PaymentReadModel>> GetPaymentsByUserId(Guid id);
    }
}