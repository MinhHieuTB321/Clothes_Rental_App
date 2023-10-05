using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class Combo:BaseEntity
    {
        public string ComboName { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public string Status { get; set; } = default!;

        //Fee
        public ICollection<Fee>? Fees { get; set; }

        //Shop
        public Guid ShopId { get; set; } = default!;
        public Shop Shop { get; set; } = default!;

        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
