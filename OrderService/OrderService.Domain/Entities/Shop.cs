using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class Shop : BaseEntity
    {
        public string ShopName { get; set; } = default!;
        public string ShopCode { get; set; } = default!;
        public string ShopEmail { get; set; } = default!;
        public string ShopPhone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Status { get; set; } = default!;
        public Guid OwnerId { get; set; }
        public ICollection<Combo>? Combos { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
