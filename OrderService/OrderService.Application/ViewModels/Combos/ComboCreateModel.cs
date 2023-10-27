namespace OrderService.Application.ViewModels.Combos
{
    public class ComboCreateModel
    {
        public Guid Id { get; set; }
        public string ComboName { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public string FileName { get; set; } = default!;
        public string FileUrl { get; set; } = default!;
        public string Status { get; set; } = default!;
        public double TotalValue { get; set; } = default!;
        public Guid ShopId { get; set; } = default!;
    }
}