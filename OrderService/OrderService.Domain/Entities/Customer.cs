using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Status {  get; set; } = default!;
        public ICollection<Order>? Orders { get; set; }
    }
}
