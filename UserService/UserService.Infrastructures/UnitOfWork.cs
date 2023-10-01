using UserService.Application;
using UserService.Application.IRepositories;

namespace UserService.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IWalletRepository _walletRepository;

        public UnitOfWork(AppDbContext context,
                        IUserRepository userRepository,
                        IOrderRepository orderRepository,
                        IPaymentRepository paymentRepository,
                        IWalletRepository walletRepository,
                        ITransactionRepository transactionRepository)
        {
            _context=context;
            _orderRepository=orderRepository;
            _paymentRepository=paymentRepository;
            _userRepository=userRepository;
            _transactionRepository=transactionRepository;
            _walletRepository=walletRepository;
        }


        public IUserRepository UserRepository => _userRepository;

        public IOrderRepository OrderRepository =>_orderRepository;

        public IPaymentRepository PaymentRepository =>_paymentRepository;

        public ITransactionRepository TransactionRepository => _transactionRepository;

        public IWalletRepository WalletRepository =>_walletRepository;

        public async Task<bool> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync()>0;
        }
    }
}