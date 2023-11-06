namespace ComboService.Domain.Entities
{
    public class Combo : BaseEntity
    {
        public string ComboName { get; set; } = default!;
        public int Quantity { get; set; } = 0;
        public string Description{get;set;}=default!;
        public string FileName { get; set; } = "ShopLogo";
        public string FileUrl { get; set; } = default!;
        public string Status { get; set; } = default!;
        public double TotalValue { get; set; } = 0;

        //PriceList
        public ICollection<PriceList>? PriceLists { get; set; } = default!;
        
        //Shop
        public Guid ShopId { get; set; } = default!;
        public Shop Shop { get; set; } = default!;

        //ProductCombo
        public ICollection<ProductCombo>? ProductCombos { get; set; } = default!;    
    }
}
