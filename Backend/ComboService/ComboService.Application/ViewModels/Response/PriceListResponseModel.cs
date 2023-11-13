using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.ViewModels.Response
{
    public class PriceListResponseModel
    {
        public Guid Id {  get; set; }
        public double Deposit { get; set; } = default!;
        public double RentalPrice { get; set; } = default!;
        public string Duration { get; set; } = default!;
        public Guid ComboId { get; set; } = default!;
    }
}
