namespace UserService.Application.ViewModels.Orders
{
    public class OrderReadModel
    {
        public Guid OrderId{get;set;}
        public Guid RecipientId{get;set;}
        public double Total{get;set;}
    }
    
}