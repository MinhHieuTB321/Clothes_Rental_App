using UserService.Application.Interfaces;
using UserService.Application.IRepositories;
using UserService.Domain.Entities;

namespace UserService.Infrastructures.Repositories
{
    public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(AppDbContext context, IClaimsService claimsService, ICurrentTime currentTime) : base(context, claimsService, currentTime)
        {
        }
    }
}