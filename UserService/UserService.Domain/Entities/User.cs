namespace UserService.Domain.Entities
{
    public class User:BaseEntity
    {
        public string Name{get;set;}=default!;

        public string Email{get;set;}=default!;
        public string Phone{get;set;}=default!;
        public string Role{get;set;}= default!;

        public ICollection<Order>? Orders{get;set;}
        public ICollection<Wallet>? Wallets{get;set;}
        public ICollection<Payment>? Payments{get;set;}
    }
}