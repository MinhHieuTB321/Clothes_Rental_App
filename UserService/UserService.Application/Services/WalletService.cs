using AutoMapper;
using UserService.Application.GlobalExceptionHandling.Exceptions;
using UserService.Application.Interfaces;
using UserService.Application.ViewModels.Transactions;
using UserService.Application.ViewModels.Wallets;
using UserService.Domain.Entities;

namespace UserService.Application.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Guid _currentUser;

        public WalletService(IUnitOfWork unitOfWork,IMapper mapper,IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = claimsService.GetCurrentUser;
        }
        public async Task<WalletReadModel> CreateWallet(WalletCreateModel model)
        {
            var wallet = _mapper.Map<Wallet>(model);
            var result= await _unitOfWork.WalletRepository.AddAsync(wallet);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<WalletReadModel>(result);
        }

        public async Task<WalletReadModel> GetWalletbyId(Guid id)
        {
            var wallet= await _unitOfWork.WalletRepository.FindByField(x=>x.Id==id&& x.UserId==_currentUser,x=>x.Transactions!);
            if (wallet == null) throw new NotFoundException($" Can not found wallet {id}");
            var result=_mapper.Map<WalletReadModel>(wallet);
            if(wallet.Transactions!.Count() > 0)
            {
                result.Transactions=_mapper.Map<List<TransactionReadModel>>(wallet.Transactions);
            }
            return result;
        }

        public async Task<bool> UpdateWallet(WalletUpdateModel model)
        {
            var wallet = await _unitOfWork.WalletRepository.FindByField(x => x.Id == model.Id && x.UserId == _currentUser, x => x.Transactions!);
            wallet = _mapper.Map(model, wallet);
            _unitOfWork.WalletRepository.Update(wallet);
            return await _unitOfWork.SaveChangeAsync();
        }
    }
}