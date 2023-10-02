using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Domain.Entities
{
    public class Product : BaseEntity
    {
        
        public string ProductName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string Size { get; set; } = default!;
        public string Color { get; set; } = default!;
        public string Material { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public decimal Compesation { get; set; } = default!;

        public Guid RootProductId { get; set; } = default!;
        public Product RootProduct { get; set; } = default!;

        public Guid ShopId { get; set; } = default!;
        public Shop Shop { get; set; } = default!;  

        public Guid CategoryId { get; set; } = default!;
        public Category Category { get; set; } = default!;

        public ICollection<ProductImage> ProductImages { get; set; } = default!;
    }
}
