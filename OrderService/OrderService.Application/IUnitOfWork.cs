using OrderService.Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application
{
    public interface IUnitOfWork
    {
        public IOrderRepository OrderRepository { get; }
        public IOrderDetailRepository OrderDetailRepository { get; }
        public IShopRepository ShopRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public IComboRepository ComboRepository { get; }
        public IFeeRepository FeeRepository { get; }
        public Task<bool> SaveChangesAsync();
    }
}
