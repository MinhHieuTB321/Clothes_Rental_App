using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Domain
{
    public class OwnerEntity:BaseEntity
    {
        public string Name { get; set; } = null;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public bool IsActive { get; set; } = false;

        public ICollection<ShopEntity>Shops { get; set; }
    }
}
