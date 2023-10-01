using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Domain
{
    public class ProductImageEntity:BaseEntity
    {
        public string ProductImageUrl { get; set; } = null!;
        public ProductEnity Product { get; set; }
    }
}
