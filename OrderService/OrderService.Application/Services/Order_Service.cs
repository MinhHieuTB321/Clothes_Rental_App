using AutoMapper;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Hangfire;
using Microsoft.Extensions.Configuration;
using OrderService.Application.GlobalExceptionHandling.Exceptions;
using OrderService.Application.Interfaces;
using OrderService.Application.ViewModels.OrderDetails;
using OrderService.Application.ViewModels.Orders;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotImplementedException = OrderService.Application.GlobalExceptionHandling.Exceptions.NotImplementedException;

namespace OrderService.Application.Services
{
    public class Order_Service : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;

        //private readonly IConfiguration _config;
        //private readonly IFirebaseConfig _fireBaseConfig;
        //private readonly IFirebaseClient _client;
        //private readonly IBackgroundJobClient _jobClient;

        //public Order_Service(IUnitOfWork unitOfWork,
        //    IMapper mapper,
        //    IClaimService claimService,
        //    IBackgroundJobClient jobClient,
        //    IConfiguration config)
        //{
        //    _unitOfWork = unitOfWork;
        //    _mapper = mapper;
        //    _claimService = claimService;
        //    _jobClient = jobClient;
        //    _config = config;
        //    _fireBaseConfig = new FirebaseConfig
        //    {
        //        AuthSecret = _config["RealTimeDatabase:AuthSecret"],
        //        BasePath = _config["RealTimeDatabase:BasePath"],
        //    };
        //    _client = new FireSharp.FirebaseClient(_fireBaseConfig);
        //}


        public Order_Service(IUnitOfWork unitOfWork,
            IMapper mapper,
            IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
        }

        public async Task<OrderReadModel> CreateOrder(OrderCreateModel model)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(_claimService.GetCurrentUser);

            var order = new Order
            {
                ShopId = model.ShopId,
                CustomerName=customer!.Name,
                CustomerAddress=customer!.Address,
                CustomerPhone=customer!.Phone,
                CustomerId = _claimService.GetCurrentUser,
                Total = model.OrderDetails.Sum(x => x.Deposit)
            };

            order = await _unitOfWork.OrderRepository.AddAsync(order);
            await CreateOrderDetail(model.OrderDetails, order.Id);
            if (await _unitOfWork.SaveChangesAsync() == false) throw new BadRequestException("Can not add order details");
            //_jobClient.Enqueue(() => AddOrderToFireBase(order.Id,_claimService.GetCurrentUser));
            return _mapper.Map<OrderReadModel>(order);
        }


        private async Task CreateOrderDetail(List<OrderDetailCreateModel> model,Guid orderId) 
        {
            
            var orderDetails = _mapper.Map<List<OrderDetail>>(model);
            for (int i = 0; i < orderDetails.Count; i++)
            {
                orderDetails[i].OrderId = orderId;
                orderDetails[i].DueDate= DateTime.Now.AddDays(model[i].Duration);
            }
             await _unitOfWork.OrderDetailRepository.AddRangeAsync(orderDetails);
        }

        //public async Task<bool> AddOrderToFireBase(Guid orderId,Guid customerId)
        //{
        //    var orderDetails= await _unitOfWork.OrderDetailRepository.FindListByField(x=>x.OrderId== orderId);
        //    var data = new OrderFireBase
        //    {
        //        OrderId= orderId,
        //        OrderDetails= _mapper.Map<List<OrderDetailFirebase>>(orderDetails)
        //    };
        //    var root = "Order-" + customerId;
        //    //PushResponse response = await _client.PushAsync($"Order/{root}/{orderId}", data);
        //    SetResponse setResponse = await _client.SetAsync($"Order/{root}/{orderId}", data);
        //    if (setResponse.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        public Task<bool> DeleteOrder(Guid id)
        {
            throw new NotImplementedException("");
        }

        public async Task<List<OrderReadModel>> GetAllOrders()
        {
            var orders= await _unitOfWork.OrderRepository.GetAllAsync(x=>x.Customer,x=>x.Shop!);
            if (orders.Count == 0) throw new NotFoundException("There are no orders exist!");
            return _mapper.Map<List<OrderReadModel>>(orders);
        }

        public async Task<OrderReadModel> GetOrderById(Guid id)
        {
            var order= await _unitOfWork.OrderRepository.GetByIdAsync(id,x=>x.Customer,x=>x.Shop!);
            if (order == null) throw new NotFoundException($"Order with ID-{id} is not exist!");
            
            var result = _mapper.Map<OrderReadModel>(order);
            result.OrderDetails = await GetOrderDetailByOrderId(id);
            return result;
        }


        public async Task<List<OrderDetailReadModel>> GetOrderDetailByOrderId(Guid id)
        {
            var orderDetails = await _unitOfWork.OrderDetailRepository.FindListByField(x => x.OrderId == id && x.IsDeleted==false, x => x.Combo);
            return _mapper.Map<List<OrderDetailReadModel>>(orderDetails);
        }

        public async Task<bool> UpdateOrder(OrderUpdateModel model)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(model.Id);
            if (order == null) throw new NotFoundException($"Order with ID-{model.Id} is not exist!");
            order = _mapper.Map(model, order);
            _unitOfWork.OrderRepository.Update(order);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
