using AutoMapper;
using OrderService.Application.GlobalExceptionHandling.Exceptions;
using OrderService.Application.Interfaces;
using OrderService.Application.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Services
{
    public class ShopService : IShopService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShopService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<OrderReadModel>> GetAllOrderyShopId(Guid shopId)
        {
            var orders = await _unitOfWork.OrderRepository.FindListByField(x => x.ShopId == shopId&&x.IsDeleted==false, x => x.Customer, x => x.Shop!);
            if (orders.Count == 0) throw new NotFoundException("There are no orders exist!");
            return _mapper.Map<List<OrderReadModel>>(orders);
        }
    }
}
