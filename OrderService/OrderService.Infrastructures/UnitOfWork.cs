using OrderService.Application;
using OrderService.Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly IShopRepository _shopRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IComboRepository _comboRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(IShopRepository shopRepository,
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IComboRepository comboRepository,
            ICustomerRepository customerRepository,
            AppDbContext appDbContext)
        {
            _shopRepository = shopRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _comboRepository = comboRepository;
            _customerRepository = customerRepository;
            _appDbContext = appDbContext;
        }

        public IOrderRepository OrderRepository => _orderRepository;

        public IOrderDetailRepository OrderDetailRepository => _orderDetailRepository;

        public IShopRepository ShopRepository => _shopRepository;

        public ICustomerRepository CustomerRepository => _customerRepository;

        public IComboRepository ComboRepository => _comboRepository;


        public async Task<bool> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync() > 0;
        }
    }
}
