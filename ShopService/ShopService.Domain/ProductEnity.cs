using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Domain
{
    public class ProductEnity:BaseEntity
    {
        public string ProductName { get; set; } = null!;
        public string ProductRoot { get; set; }= null!;
        public string Description { get; set; } = null!;
        public string Size { get; set; } = null!;
        public string Color { get;set; } = null!;
        public string Material { get; set; } = null!;
        public decimal Compesation { get; set; }=default(decimal);
        public ShopEntity Shop { get; set; }
        public ProductEnity Product { get; set; }
        public CategoryEntity Category { get; set; }
        public ICollection<ProductImageEntity> ProductImages { get; set; }
        public ICollection<ProductEnity> Products { get; set; } 
    }
}
