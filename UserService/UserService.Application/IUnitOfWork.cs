using UserService.Application.IRepositories;

namespace UserService.Application
{
    
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository {get;}
        public IOrderRepository OrderRepository{get;}
        public IPaymentRepository PaymentRepository {get;}
        public ITransactionRepository TransactionRepository {get;}
        public IWalletRepository WalletRepository {get;}
        
        public Task<bool> SaveChangeAsync();
    }
}