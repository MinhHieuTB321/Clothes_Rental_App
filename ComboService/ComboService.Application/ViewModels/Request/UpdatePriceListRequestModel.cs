using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.ViewModels.Request
{
    public class UpdatePriceListRequestModel
    {
        public Guid PriceListId { get; set; } = default!;
        public decimal Deposit { get; set; } = default!;
        public decimal RentalPrice { get; set; } = default!;
    }
}
