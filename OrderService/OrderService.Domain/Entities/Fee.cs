using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class Fee:BaseEntity
    { 
        public double Deposit { get; set; } = default!;
        public double RentalPrice { get; set; } = default!;
        public int Duration { get; set; } = default!;
        public Guid ComboId { get; set; } = default!;
        public Combo Combo { get; set; } = default!;
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
