using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.ViewModels.OrderDetails
{
    public class OrderDetailCreateModel
    {
        public Guid ComboId { get; set; }
        public int Duration { get; set; }
        public double Deposit { get; set; } = default!;
        public double RentalPrice { get; set; } = default!;
    }
}
