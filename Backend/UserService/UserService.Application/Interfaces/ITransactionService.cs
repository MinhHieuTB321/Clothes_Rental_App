using UserService.Application.ViewModels.Transactions;
using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionReadModel> CreateTransaction(TransactionCreateModel createModel);
        Task<TransactionReadModel> GetTransactionById(Guid id,Guid paymentId);

        Task<List<TransactionReadModel>> GetAllTransactions(Guid paymentId);
    }
}