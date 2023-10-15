using OrderService.Application.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Interfaces
{
    public interface IShopService
    {
        Task<List<OrderReadModel>> GetAllOrderyShopId(Guid shopId);
    }
}
