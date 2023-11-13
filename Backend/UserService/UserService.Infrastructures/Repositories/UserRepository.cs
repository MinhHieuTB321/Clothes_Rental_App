using UserService.Application.Interfaces;
using UserService.Application.IRepositories;
using UserService.Domain.Entities;

namespace UserService.Infrastructures.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context, IClaimsService claimsService, ICurrentTime currentTime) : base(context, claimsService, currentTime)
        {
        }
    }
}