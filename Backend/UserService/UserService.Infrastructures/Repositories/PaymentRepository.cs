using UserService.Application.Interfaces;
using UserService.Application.IRepositories;
using UserService.Domain.Entities;

namespace UserService.Infrastructures.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext context, IClaimsService claimsService, ICurrentTime currentTime) : base(context, claimsService, currentTime)
        {
        }
    }
}