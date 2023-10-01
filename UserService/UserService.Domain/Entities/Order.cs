namespace UserService.Domain.Entities
{
    public class Order:BaseEntity
    {
        public Guid ExternalId {get;set;}
        public double Total{get;set;}
        public Guid PayerId{get;set;}
        public User? Payer{get;set;}
        public Guid RecipientId{get;set;}
        public ICollection<Payment>? Payments{get;set;}
        
    }
}