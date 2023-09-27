using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Domain.Entities
{
    public class ShopLogo : BaseEntity
    {
        public string FileName { get; set; } = default!;
        public string FileUrl { get; set; } = default!;
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; } = default!;
    }
}
