namespace OrderService.Application.ViewModels.Shops
{
    public class ShopCreateModel
    {
        public Guid Id { get; set; }
        public string ShopName { get; set; } = default!;
        public string ShopCode { get; set; } = default!;
        public string ShopEmail { get; set; } = default!;
        public string ShopPhone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public Guid OwnerId { get; set; } = default!;
        public string Status { get; set; } = default!;
    }
}