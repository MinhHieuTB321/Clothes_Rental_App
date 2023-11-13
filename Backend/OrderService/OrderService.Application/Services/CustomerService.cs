using AutoMapper;
using OrderService.Application.GlobalExceptionHandling.Exceptions;
using OrderService.Application.Interfaces;
using OrderService.Application.ViewModels.Customers;
using OrderService.Application.ViewModels.Orders;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;

        public CustomerService(IUnitOfWork unitOfWork,IMapper mapper, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;

        }

        public async Task<CustomerReadModel> CreateCustomer(CustomerCreateModel model)
        {
            var customer = _mapper.Map<Customer>(model);
            var result= await _unitOfWork.CustomerRepository.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CustomerReadModel>(result);
        }

        public async Task<bool> DeleteCustomer(Guid id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
            if(customer == null) throw new NotFoundException($"Customer with ID-{id} is not exist!");
            _unitOfWork.CustomerRepository.SoftRemove(customer);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<CustomerReadModel>> GetAllCustomers()
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
            if (customers.Count == 0) throw new NotFoundException($"There are no customer available");
            return _mapper.Map<List<CustomerReadModel>>(customers);
        }

        public async Task<List<OrderReadModel>> GetAllOrderByCustomerID(Guid id)
        {
            if (id != _claimService.GetCurrentUser) throw new BadRequestException("Id is not match with current user!");
            var orders = await _unitOfWork.OrderRepository.FindListByField(x=>x.CustomerId==id&&x.IsDeleted==false,x => x.Customer, x => x.Shop!);
            if (orders.Count == 0) throw new NotFoundException("There are no orders exist!");
            return _mapper.Map<List<OrderReadModel>>(orders);
        }

        public async Task<CustomerReadModel> GetCustomerById(Guid id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
            if (customer == null) throw new NotFoundException($"Customer with ID-{id} is not exist!");
            return _mapper.Map<CustomerReadModel>(customer);
        }

        public async Task<bool> UpdateCustomer(CustomerUpdateModel model)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(model.Id);
            if (customer == null) throw new NotFoundException($"Customer with ID-{model.Id} is not exist!");
            customer=  _mapper.Map(model, customer);
            _unitOfWork.CustomerRepository.Update(customer);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
