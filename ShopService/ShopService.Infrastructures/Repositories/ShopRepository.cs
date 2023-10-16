using ShopService.Application.Interfaces;
using ShopService.Application.IRepositories;
using ShopService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Infrastructures.Repositories
{
    public class ShopRepository : GenericRepository<Shop>, IShopRepository
    {
        public ShopRepository(AppDbContext context, IClaimService claimsService, ICurrentTime currentTime) : base(context, claimsService, currentTime)
        {
        }
    }
}
