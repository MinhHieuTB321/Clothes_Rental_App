using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; } = null!;
        public ICollection<Product>? Products { get; set; }
    }
}
