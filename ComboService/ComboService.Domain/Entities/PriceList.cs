using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Domain.Entities
{
    public class PriceList : BaseEntity
    {
        public Guid PriceListId { get; set; } = default!;
        public decimal Deposit { get; set; } = default!;
        public decimal RentalPrice { get; set; } = default!;
        public string Duration { get; set; } = default!;

        public Combo Combo { get; set; } = default!;
    }
}
