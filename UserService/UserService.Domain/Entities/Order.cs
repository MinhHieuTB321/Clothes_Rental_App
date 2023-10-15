namespace UserService.Domain.Entities
{
    public class Order:BaseEntity
    {
        public double Total{get;set;}
        public Guid CustomerId{get;set;}
        public User? Customer{get;set;}
        public Guid OwnerId{get;set;}
        public ICollection<Payment>? Payments{get;set;}
        
    }
}