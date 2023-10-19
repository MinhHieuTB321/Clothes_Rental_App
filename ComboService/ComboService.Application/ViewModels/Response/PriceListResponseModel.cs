using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.ViewModels.Response
{
    public class PriceListResponseModel
    {
        public decimal Deposit { get; set; } = default!;
        public decimal RentalPrice { get; set; } = default!;
        public string Duration { get; set; } = default!;
        public Guid ComboId { get; set; } = default!;
    }
}
