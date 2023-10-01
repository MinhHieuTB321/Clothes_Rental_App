namespace  UserService.Application.ViewModels.Payments
{
    public class PaymentReadModel
    {
        public Guid Id{get;set;}
        //public Guid RecipientId{get;set;}
        public Guid PartyId{get;set;}
        public Guid OrderId{get;set;}
        public string? PartyName{get;set;}
        public string? Phone { get; set; }
        public double Amount{get;set;}
        public string? Method { get; set; }
        public string? Status{get;set;}
        public string? Type { get; set; }
        public DateTime CreationDate { get; set; }
    }
}