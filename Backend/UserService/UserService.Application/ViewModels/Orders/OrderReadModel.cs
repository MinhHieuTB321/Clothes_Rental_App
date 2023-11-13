namespace UserService.Application.ViewModels.Orders
{
    public class OrderReadModel
    {
        public Guid Id { get; set; }
        public double Total { get; set; }
        public string? Status { get; set; }
        public Guid CustomerId { get; set; }
        public Guid OwnerId { get; set; }
    }
    
}