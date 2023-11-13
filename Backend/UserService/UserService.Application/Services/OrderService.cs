using AutoMapper;
using UserService.Application.GlobalExceptionHandling.Exceptions;
using UserService.Application.Interfaces;
using UserService.Application.ViewModels.Payments;

namespace UserService.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly IClaimsService _claimsService;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
        }

        public async Task<List<PaymentReadModel>> GetAllPaymentsByOrderId(Guid orderId)
        {
            var payments = await _unitOfWork
                .PaymentRepository
                .FindListByField(x => x.CustomerId == _claimsService.GetCurrentUser && x.OrderId==orderId, x => x.Transactions!, x => x.Customer!);
            if (payments.Count == 0) throw new NotFoundException("Payments are not exist!");
            var result = _mapper.Map<List<PaymentReadModel>>(payments);
            return result;
        }
    }
}