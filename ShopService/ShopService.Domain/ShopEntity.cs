using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Domain
{
    public class ShopEntity:BaseEntity
    {
        public string ShopName { get; set; } = null;
        public string ShopEmail { get; set; } = null;
        public string ShopPhone { get; set; } = null;
        public string Logo { get; set; } = null;
        public string ShopAddress { get; set; } = null;
        public bool IsActive { get; set; } = false;
        public OwnerEntity Owner { get; set; }
        public ICollection<ProductEnity> Products { get; set; }
    }
}
