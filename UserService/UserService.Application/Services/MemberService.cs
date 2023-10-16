using AutoMapper;
using System.Data;
using UserService.Application.GlobalExceptionHandling.Exceptions;
using UserService.Application.Interfaces;
using UserService.Application.ViewModels.Payments;
using UserService.Application.ViewModels.Users;
using UserService.Domain.Entities;
using UserService.Domain.Enums;
using NotImplementedException = UserService.Application.GlobalExceptionHandling.Exceptions.NotImplementedException;

namespace UserService.Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<UserReadModel>> GetAllUserByRole(string role)
        {
            var user = await _unitOfWork.UserRepository.FindListByField(x => x.Role == role, x => x.Wallets!);
            var result = _mapper.Map<List<UserReadModel>>(user);
            for (int i = 0; i < result.Count; i++)
            {
                var wallet = user[i].Wallets!.FirstOrDefault(x => x.IsDeleted == false);
                result[i].WalletId = wallet!.Id;
                result[i].Balance = wallet!.Balance;
            }

            return result;
        }


        private async Task<PaymentReadModel> GetOwnertyById(PaymentReadModel payment)
        {
            var owner = await _unitOfWork.UserRepository.GetByIdAsync(payment.OwnerId);
            if (owner == null) throw new NotFoundException($"Owner is not exist {payment.OwnerId}");
            payment.OwnerName = owner.Name;
            payment.Phone = owner.Phone;
            return payment;
        }


        public async Task<List<PaymentReadModel>> GetPaymentsByCustomerId(Guid userId)
        {
            var payments = await _unitOfWork.PaymentRepository.FindListByField(x => x.CustomerId == userId && x.Type!.Equals("Transfer"), x => x.Transactions!);

            var result = _mapper.Map<List<PaymentReadModel>>(payments);
            if (result.Count == 0) return result;
            for (int i = 0; i < result.Count; i++)
            {
                result[i] = await GetOwnertyById(result[i]);
            }
            return result;
        }

        public async Task<UserReadModel> GetCustomerById(Guid id)
        {
            var user = await _unitOfWork.UserRepository.FindByField(x => x.Id == id, x => x.Wallets!);
            var result = _mapper.Map<UserReadModel>(user);
            result.Payments = await GetPaymentsByCustomerId(result.Id);
            var wallet = user.Wallets!.FirstOrDefault(x => x.IsDeleted == false);
            result.WalletId = wallet!.Id;
            result.Balance = wallet!.Balance;
            return result;
        }

        public async Task<UserReadModel> GetOwnerById(Guid id)
        {
            var user = await _unitOfWork.UserRepository.FindByField(x => x.Id == id, x => x.Wallets!);
            var result = _mapper.Map<UserReadModel>(user);
            result.Payments = await GetPaymentsByOwnerId(result.Id);
            var wallet = user.Wallets!.FirstOrDefault(x => x.IsDeleted == false);
            result.WalletId = wallet!.Id;
            result.Balance = wallet!.Balance;
            return result;
        }

        public async Task<List<PaymentReadModel>> GetPaymentsByOwnerId(Guid id)
        {
            var payments = await _unitOfWork.PaymentRepository.FindListByField(x => x.OwnerId == id && x.Type!.Equals("Receive"), x => x.Transactions!);

            var result = _mapper.Map<List<PaymentReadModel>>(payments);
            if (result.Count == 0) return result;
            for (int i = 0; i < result.Count; i++)
            {
                result[i] = await GetOwnertyById(result[i]);
            }
            return result;
        }

        public async Task<List<PaymentReadModel>> GetPaymentsByUserId(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            var result = new List<PaymentReadModel>();
            if (user == null) throw new NotFoundException($"User with Id-{id} is not exist!");
            switch (user.Role)
            {
                case nameof(RoleEnums.Customer):
                    return await GetPaymentsByCustomerId(id); 
                case nameof(RoleEnums.Owner):
                    return await GetPaymentsByOwnerId(id);
                default:
                    throw new NotImplementedException("");
            }
        }
    }
}