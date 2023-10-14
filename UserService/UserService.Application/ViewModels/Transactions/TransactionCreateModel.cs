namespace UserService.Application.ViewModels.Transactions
{
    public class TransactionCreateModel
    {
        public Guid OrderId { get; set; }
        public Guid PaymentId{get;set;}
        public Guid OwnerId { get; set; }
        public double Amount{get;set;}
        public string? Method { get; set; }
    }
}