namespace UserService.Application.ViewModels.Orders
{
    public class OrderUpdatePublishedModel
    {
        public Guid Id { get; set; }
        public string? Event { get; set; } = "OrderUpdate_Published";
    }
    
}