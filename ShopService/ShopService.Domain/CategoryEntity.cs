using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Domain
{
    public class CategoryEntity:BaseEntity
    {
        public string CategoryName { get; set; } = null!;
        public ICollection<ProductEnity> Products { get; set;}
    }
}
