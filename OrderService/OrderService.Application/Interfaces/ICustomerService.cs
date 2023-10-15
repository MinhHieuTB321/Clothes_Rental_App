using OrderService.Application.ViewModels.Customers;
using OrderService.Application.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerReadModel> GetCustomerById(Guid id);
        Task<List<CustomerReadModel>> GetAllCustomers();
        Task<CustomerReadModel> CreateCustomer(CustomerCreateModel model);
        Task<bool> UpdateCustomer(CustomerUpdateModel model);
        Task<bool> DeleteCustomer(Guid id);
        Task<List<OrderReadModel>> GetAllOrderByCustomerID(Guid id);
    }
}
