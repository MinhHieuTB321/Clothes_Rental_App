using OrderService.Application.Interfaces;
using OrderService.Application.IRepositories;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructures.Repositories
{
    public class FeeRepository : GenericRepository<Fee>, IFeeRepository
    {
        public FeeRepository(AppDbContext context, IClaimService claimsService, ICurrentTime currentTime) : base(context, claimsService, currentTime)
        {
        }
    }
}
