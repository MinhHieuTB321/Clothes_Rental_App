namespace UserService.Domain.Entities
{
    public class Wallet:BaseEntity
    {
        public double Balance{get;set;}
        public string? Description{get;set;}
        public string? Status{get;set;}
        public Guid UserId{get;set;}
        public User? User{get;set;}
        public ICollection<Transaction>? Transactions{get;set;}
    }
}