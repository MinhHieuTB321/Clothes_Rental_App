using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Domain.Entities
{
    public class Shop : BaseEntity
    {
        public Guid ShopId { get; set; } = default!;
        public string ShopName { get; set; } = default!;
        public string ShopCode { get; set; } = default!;
        public string ShopEmail { get; set; } = default!;
        public string ShopPhone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Status { get; set; } = default!;

        public ICollection<ShopLogo> Logo { get; set; } = default!;

        public ICollection<Combo> Combos { get; set; } = default!;

    }
}
