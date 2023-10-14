using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Domain.Entities
{
    public class Combo : BaseEntity
    {
        public string ComboName { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public string Status { get; set; } = default!;
        public decimal TotalValue { get; set; } = default!;

        //PriceList
        public Guid PriceListId { get; set; } = default!;
        public ICollection<PriceList> PriceLists { get; set; } = default!;
        
        //Shop
        public Guid ShopId { get; set; } = default!;
        public Shop Shop { get; set; } = default!;

        //ProductCombo
        public ICollection<ProductCombo> ProductCombos { get; set; } = default!;    
    }
}
