using AutoMapper;
using System.Data;
using UserService.Application.GlobalExceptionHandling.Exceptions;
using UserService.Application.Interfaces;
using UserService.Application.ViewModels.Payments;
using UserService.Application.ViewModels.Users;

namespace UserService.Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemberService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<UserReadModel>> GetAllUserByRole(string role)
        {
            var user = await _unitOfWork.UserRepository.FindListByField(x=>x.Role == role,x=>x.Wallets!);
            var result= _mapper.Map<List<UserReadModel>>(user);
            for (int i = 0; i < result.Count; i++)
            {
                var wallet = user[i].Wallets!.FirstOrDefault(x => x.IsDeleted == false);
                result[i].WalletId = wallet!.Id;
                result[i].Balance=wallet!.Balance;
            }

            return result;
        }


        private async Task<PaymentReadModel> GetPartyById(PaymentReadModel payment)
        {
            var party = await _unitOfWork.UserRepository.GetByIdAsync(payment.PartyId);
            if (party == null) throw new NotFoundException($"Party is not exist {payment.PartyId}");
            payment.PartyName = party.Name;
            payment.Phone = party.Phone;
            return payment;
        }


        public async Task<List<PaymentReadModel>> GetPaymentsByUserId(Guid userId)
        {
            var payments = await _unitOfWork.PaymentRepository.FindListByField(x => x.UserId == userId, x => x.Transactions!);
            if (payments.Count == 0) throw new NotFoundException("Payments are not exist!");
            var result = _mapper.Map<List<PaymentReadModel>>(payments);
            for (int i = 0; i < result.Count; i++)
            {
                result[i] = await GetPartyById(result[i]);
            }
            return result;
        }

        public async Task<UserReadModel> GetUserById(Guid id)
        {
            var user = await _unitOfWork.UserRepository.FindByField(x => x.Id == id, x => x.Wallets!);
            var result = _mapper.Map<UserReadModel>(user);
            result.Payments = await GetPaymentsByUserId(result.Id);
            var wallet = user.Wallets!.FirstOrDefault(x => x.IsDeleted == false);
            result.WalletId = wallet!.Id;
            result.Balance = wallet!.Balance;
            return result;
        }
    }
}