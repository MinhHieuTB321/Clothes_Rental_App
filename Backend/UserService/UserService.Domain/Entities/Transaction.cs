using UserService.Domain.Enums;

namespace UserService.Domain.Entities
{
    public class Transaction:BaseEntity
    {
        public double Amount{get;set;}
        public string? Status{get;set;}=TransactionEnums.Success.ToString();
        public string? Type { get; set; } 
        public Guid PaymentId{get;set;}
        public Payment? Payment{get;set;}
        public Guid WalletId{get;set;}
        public Wallet? Wallet{get;set;}
    }
}