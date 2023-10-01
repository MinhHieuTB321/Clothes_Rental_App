using UserService.Domain.Enums;

namespace UserService.Domain.Entities
{
    public class Payment:BaseEntity
    {
        public string? Method{get;set;}
        public string? Status{get;set;}=PaymentEnums.Pending.ToString();
        public string? Type { get;set;}= PaymentTypeEnums.Transfer.ToString();
        public double Amount{get;set;}
        public string? Note{get;set;}
        public Guid OrderId{get;set;}
        public Order? Order {get;set;}
        public Guid PartyId{get;set;}
        public Guid UserId{get;set;}
        public User? User{get;set;}
        public ICollection<Transaction>? Transactions{get;set;}
    }
}