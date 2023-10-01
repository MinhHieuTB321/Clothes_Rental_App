using AutoMapper;
using UserService.Application.GlobalExceptionHandling.Exceptions;
using UserService.Application.Interfaces;
using UserService.Application.ViewModels.Transactions;
using UserService.Domain.Entities;
using UserService.Domain.Enums;

namespace UserService.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly IClaimsService _claimsService;

        public TransactionService(IUnitOfWork unitOfWork,IMapper mapper,IClaimsService claimsService)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;    
            _claimsService=claimsService;        
        }
        public async Task<TransactionReadModel> CreateTransaction(TransactionCreateModel createModel)
        {
            //Add Transaction
            var result = await AddTransactionForPayer(createModel);
            if (result.Status==TransactionEnums.Error.ToString())
            {
                await AddTransactionForParty(createModel);
            }
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<TransactionReadModel>(result);
        }
        // Add Transaction for Party

        private async Task AddTransactionForParty(TransactionCreateModel createModel)
        {
            var payment = await AddPaymentForParty(createModel);
            var wallet = await _unitOfWork.WalletRepository.FindByField(x => x.UserId == createModel.PartyId);
            if (wallet == null) throw new NotFoundException($"Can not find wallet of user {createModel.PartyId}!");
            var tran = new Transaction
            {
                Type=PaymentTypeEnums.Transfer.ToString(),
                Amount = createModel.Amount,
                PaymentId = payment.Id,
                WalletId = wallet.Id
            };
            var result = await _unitOfWork.TransactionRepository.AddAsync(tran);
            UpdateWallet(result, wallet,true);
        }

        private async Task<Payment> AddPaymentForParty(TransactionCreateModel createModel)
        {
            var payment = _mapper.Map<Payment>(createModel);
            payment.PartyId = _claimsService.GetCurrentUser;
            payment.Type=PaymentTypeEnums.Receive.ToString();
            var result = await _unitOfWork.PaymentRepository.AddAsync(payment);
            return result;
        }
        // Add Transaction for Payer
        private async Task<Transaction> AddTransactionForPayer(TransactionCreateModel createModel)
        {
            var flag = true;
            var paymentStatus= PaymentEnums.Success.ToString();
            var wallet = await _unitOfWork.WalletRepository.FindByField(x => x.UserId == _claimsService.GetCurrentUser);
            if (wallet == null) throw new NotFoundException($"Can not find wallet of user {_claimsService.GetCurrentUser}!");
            var tran = new Transaction
            {
                Type = PaymentTypeEnums.Receive.ToString(),
                Amount = createModel.Amount,
                PaymentId = createModel.PaymentId,
                WalletId = wallet.Id
            };
            if (wallet.Balance < createModel.Amount)
            {
                tran.Status=TransactionEnums.Error.ToString();
                flag = false;
                paymentStatus = PaymentEnums.Fail.ToString();
            }
            var result = await _unitOfWork.TransactionRepository.AddAsync(tran);
            UpdateWallet(result, wallet,flag);
            await  UpdatePaymentPayer(createModel.PaymentId, paymentStatus);
            return result;
        }

        private async Task UpdatePaymentPayer(Guid id,string status)
        {
            var payment= await _unitOfWork.PaymentRepository.GetByIdAsync(id);
            if (payment == null) throw new NotFoundException($"Can not found payment {id}");
            payment!.Status= status;
            _unitOfWork.PaymentRepository.Update(payment);
        }
        

        private void UpdateWallet(Transaction tran, Wallet wallet,bool isValid)
        {
            if (isValid == false) return;

            if (tran.Type == PaymentTypeEnums.Transfer.ToString())
            {
                wallet.Balance -= tran.Amount;
            }
            else
            {
                wallet.Balance += tran.Amount;
            }
            _unitOfWork.WalletRepository.Update(wallet);
        }

        public async Task<List<TransactionReadModel>> GetAllTransactions(Guid paymentId)
        {
            var trans= await _unitOfWork
                .TransactionRepository
                .FindListByField(x=>x.CreatedBy==_claimsService.GetCurrentUser&& x.PaymentId==paymentId);
            return _mapper.Map<List<TransactionReadModel>>(trans);
        }

        public async Task<TransactionReadModel> GetTransactionById(Guid id, Guid paymentId)
        {
            var tran = await _unitOfWork
                .TransactionRepository
                .FindByField(x=>x.Id==id && x.CreatedBy==_claimsService.GetCurrentUser && x.PaymentId==paymentId);
            return _mapper.Map<TransactionReadModel>(tran);
        }

    }
}