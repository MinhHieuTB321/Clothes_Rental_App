using System.Collections.Generic;
using UserService.Application.ViewModels.Payments;
using UserService.Application.ViewModels.Users;

namespace UserService.Application.Interfaces
{
    public interface IMemberService
    {
        Task<List<UserReadModel>> GetAllUserByRole(string role);
        Task<UserReadModel> GetUserById(Guid id);

        Task<List<PaymentReadModel>> GetPaymentsByUserId(Guid id);
    }
}