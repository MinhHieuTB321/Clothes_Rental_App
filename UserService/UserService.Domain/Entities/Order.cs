namespace UserService.Domain.Entities
{
    public class Order:BaseEntity
    {
        public Guid ExternalId {get;set;}
        public double Total{get;set;}
        public ICollection<Payment>? Payments{get;set;}
    }
}