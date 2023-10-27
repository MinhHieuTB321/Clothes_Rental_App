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

        public async Task<UserReadModel> GetUserById(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id, x => x.Wallets!);
            if(user==null) throw new NotFoundException($"User with Id-{id} is not exist!");
            var result = _mapper.Map<UserReadModel>(user);
            var wallet = user.Wallets!.FirstOrDefault(x => x.IsDeleted == false);
            result.WalletId = wallet!.Id;
            result.Balance = wallet!.Balance; 
            result.Payments=await GetPaymentsByUserId(id);
            return result;
        }


        public async Task<List<PaymentReadModel>> GetPaymentsByUserId(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null) throw new NotFoundException($"User with Id-{id} is not exist!");
            switch (user.Role)
            {
                case nameof(RoleEnums.Customer):
                    return await GetPaymentsByCustomerId(id); 
                case nameof(RoleEnums.Owner):
                    return await GetPaymentsByOwnerId(id);
                default:
                    return new List<PaymentReadModel>();
            }
        }

        public async Task<List<PaymentReadModel>> GetPaymentsByCustomerId(Guid userId)
        {
            var payments = await _unitOfWork.PaymentRepository.FindListByField(x => x.CustomerId == userId && x.Type!.Equals("Transfer"), x => x.Transactions!);

            var result = _mapper.Map<List<PaymentReadModel>>(payments);
            if (result.Count == 0) return result;
            for (int i = 0; i < result.Count; i++)
            {
                result[i] = await GetOwnerById(result[i]);
            }
            return result;
        }

        public async Task<List<PaymentReadModel>> GetPaymentsByOwnerId(Guid id)
        {
            var payments = await _unitOfWork.PaymentRepository.FindListByField(x => x.OwnerId == id && x.Type!.Equals("Receive"), x => x.Transactions!);

            var result = _mapper.Map<List<PaymentReadModel>>(payments);
            if (result.Count == 0) return result;
            for (int i = 0; i < result.Count; i++)
            {
                result[i] = await GetOwnerById(result[i]);
            }
            return result;
        }

      
        private async Task<PaymentReadModel> GetOwnerById(PaymentReadModel payment)
        {
            var owner = await _unitOfWork.UserRepository.GetByIdAsync(payment.OwnerId);
            if (owner == null) throw new NotFoundException($"Owner is not exist {payment.OwnerId}");
            payment.OwnerName = owner.Name;
            payment.Phone = owner.Phone;
            return payment;
        }


        public async Task<UserReadModel> CreateUser(UserCreateModel model)
        {
            var user= _mapper.Map<User>(model);
            user= await _unitOfWork.UserRepository.AddAsync(user);
            var wallet= await AddWallet(user.Id);
            await _unitOfWork.SaveChangeAsync();
            var result= _mapper.Map<UserReadModel>(user);
            result.WalletId=wallet.Id;
            result.Balance=wallet.Balance;
            return result;
        }

        private async Task<Wallet> AddWallet(Guid userId){
            var wallet= new Wallet{
                UserId=userId,
                Balance=1
            };

           return await _unitOfWork.WalletRepository.AddAsync(wallet);
        }


		public async Task<UserReadModel> UpdateUser(UserUpdateModel model)
		{
			var user = await _unitOfWork.UserRepository.GetByIdAsync(model.Id, x => x.Wallets!);
			if (user == null) throw new NotFoundException($"User with Id-{model.Id} is not exist!");
            user = _mapper.Map(model, user);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<UserReadModel>(user);
		}

		public async Task<UserReadModel> DeleteUser(Guid id)
		{
			var user = await _unitOfWork.UserRepository.GetByIdAsync(id, x => x.Wallets!);
			if (user == null) throw new NotFoundException($"User with Id-{id} is not exist!");
			_unitOfWork.UserRepository.SoftRemove(user);
			await _unitOfWork.SaveChangeAsync();
			return _mapper.Map<UserReadModel>(user);
		}
	}
}