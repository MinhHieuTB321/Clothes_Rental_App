using UserService.Application.Interfaces;
using UserService.Application.IRepositories;
using UserService.Domain.Entities;

namespace UserService.Infrastructures.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context, IClaimsService claimsService, ICurrentTime currentTime) : base(context, claimsService, currentTime)
        {
        }
    }
}