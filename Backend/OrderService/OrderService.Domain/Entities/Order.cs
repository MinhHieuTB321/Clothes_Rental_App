using OrderService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class Order:BaseEntity
    {
        public double Total {  get; set; }
        public string? Note { get; set; } = default!;
        public string CustomerName { get; set; } = default!;
        public string CustomerPhone { get; set; } = default!;
        public string CustomerAddress { get; set; } = default!;
        public string Status {  get; set; }=OrderEnum.Pending.ToString()!;

        //SubOrder
        public Guid? ShopId { get; set; }
        public Shop? Shop { get; set; }

        public Guid CustomerId {  get; set; }
        public Customer Customer { get; set; } = default!;

        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
