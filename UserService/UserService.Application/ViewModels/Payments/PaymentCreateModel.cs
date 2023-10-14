namespace UserService.Application.ViewModels.Payments
{
    public class PaymentCreateModel
    {
        public string? Method{get;set;}
        public double Amount{get;set;}
        public Guid OrderId{get;set;}
        public Guid OwnerId { get;set;}
    }
}