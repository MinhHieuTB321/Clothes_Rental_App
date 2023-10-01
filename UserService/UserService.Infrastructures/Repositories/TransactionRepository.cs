using UserService.Application.Interfaces;
using UserService.Application.IRepositories;
using UserService.Domain.Entities;

namespace UserService.Infrastructures.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AppDbContext context, IClaimsService claimsService, ICurrentTime currentTime) : base(context, claimsService, currentTime)
        {
        }
    }
}