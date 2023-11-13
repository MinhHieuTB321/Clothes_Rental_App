using AutoMapper;
using UserService.Application.GlobalExceptionHandling.Exceptions;
using UserService.Application.Interfaces;
using UserService.Application.ViewModels.Payments;
using UserService.Domain.Entities;
using UserService.Domain.Enums;

namespace UserService.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly IClaimsService _claimsService;

        public PaymentService(IUnitOfWork unitOfWork,IMapper mapper,IClaimsService claimsService)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;    
            _claimsService=claimsService;        
        }
        public async Task<PaymentReadModel> CreatePayment(PaymentCreateModel createModel)
        {
            var payment= _mapper.Map<Payment>(createModel);
            payment.CustomerId=_claimsService.GetCurrentUser;
            payment = await _unitOfWork.PaymentRepository.AddAsync(payment);
            await _unitOfWork.SaveChangeAsync();
            var result= _mapper.Map<PaymentReadModel>(payment);
            return result;
        }

        public async Task<List<PaymentReadModel>> GetAllPayments()
        {
            var payments = await _unitOfWork.PaymentRepository.FindListByField(x=>x.CustomerId==_claimsService.GetCurrentUser,x => x.Transactions!);
            if (payments.Count==0) throw new NotFoundException("Payments are not exist!");
            var result = _mapper.Map<List<PaymentReadModel>>(payments);
            for (int i = 0; i < result.Count; i++)
            {
                result[i] = await GetPartyById(result[i]);
            }
            return result;
        }

        private async Task<PaymentReadModel> GetPartyById(PaymentReadModel payment)
        {
            var party = await _unitOfWork.UserRepository.GetByIdAsync(payment.OwnerId);
            if (party == null) throw new NotFoundException($"Party is not exist {payment.OwnerId}");
            payment.OwnerName = party.Name;
            payment.Phone = party.Phone;
            return payment;
        }

        public async Task<PaymentReadModel> GetPaymentById(Guid id)
        {
            var payment = await _unitOfWork.PaymentRepository.FindByField(x => x.Id == id, x => x.Transactions!, x => x.Customer!);
            if (payment == null) throw new NotFoundException("Payment is not exist!");
            var result = _mapper.Map<PaymentReadModel>(payment);
            var party = await _unitOfWork.UserRepository.GetByIdAsync(payment.OwnerId);
            if (party == null) throw new NotFoundException($"Party is not exist {payment.OwnerId}");
            result.OwnerName = party.Name;
            result.Phone = party.Phone;
            return result;
        }

    }
}