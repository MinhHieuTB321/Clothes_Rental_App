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
        public Guid OwnerId{get;set;}
        public User? Owner { get; set; }
        public Guid CustomerId{get;set;}
        public User? Customer{get;set;}
        public ICollection<Transaction>? Transactions{get;set;}
    }
}