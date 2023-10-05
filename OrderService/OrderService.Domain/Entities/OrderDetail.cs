using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public string Note { get; set; } = default!;
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = default!;

        public Guid OrderId { get; set; }
        public Order Order { get; set; } = default!;

        public Guid ComboId { get; set; }
        public Combo Combo { get; set; } = default!;

        public Guid FeeId { get; set; }
        public Fee Fee { get; set; } = default!;

        public Guid ShopId { get; set; }
        public Shop Shop { get; set; } = default!;
    }
}
