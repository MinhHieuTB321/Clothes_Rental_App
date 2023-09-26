namespace UserService.Domain.Entities
{
    public class Order:BaseEntity
    {
        public Guid ExternalId {get;set;}
        public double Total{get;set;}
        public Guid UserId{get;set;}
        public User? User{get;set;}
        public ICollection<Payment>? Payments{get;set;}
        
    }
}