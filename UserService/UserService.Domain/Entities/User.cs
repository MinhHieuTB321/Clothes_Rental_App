namespace UserService.Domain.Entities
{
    public class User:BaseEntity
    {
        public Guid ExternalId{get;set;}
        public string Name{get;set;}=default!;

        public string Email{get;set;}=default!;
        public string Role{get;set;}= default!;

        public ICollection<Order>? Order{get;set;}
        public ICollection<Wallet>? Wallet{get;set;}
        public ICollection<Payment>? Payment{get;set;}
    }
}