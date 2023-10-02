using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Domain.Entities
{
    public class Owner : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Status { get; set; } = default!;

        public ICollection<Shop>? Shops { get; set; }
    }
}
