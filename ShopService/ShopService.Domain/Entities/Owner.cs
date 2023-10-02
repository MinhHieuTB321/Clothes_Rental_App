using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Domain.Entities
{
    public class Owner : BaseEntity
    {
        public string Name { get; set; } = null;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string IsActive { get; set; } = null!;

        public ICollection<Shop> Shop { get; set; }
    }
}
