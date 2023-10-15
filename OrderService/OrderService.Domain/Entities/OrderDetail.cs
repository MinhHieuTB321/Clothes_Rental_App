using OrderService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public DateTime DueDate { get; set; }
        public double Deposit { get; set; } = default!;
        public double RentalPrice { get; set; } = default!;
        public string Status { get; set; } = OrderDetailEnum.Pending.ToString()!;

        public Guid OrderId { get; set; }
        public Order Order { get; set; } = default!;

        public Guid ComboId { get; set; }
        public Combo Combo { get; set; } = default!;

    }
}
