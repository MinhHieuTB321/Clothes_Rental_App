using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class Order:BaseEntity
    {
        public Guid ShopOwnerId {  get; set; }
        public double Total {  get; set; }
        public string Note { get; set; } = default!;

        public Guid CustomerId {  get; set; }
        public Customer Customer { get; set; } = default!;
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
