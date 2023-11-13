using OrderService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.ViewModels.Orders
{
    public class OrderUpdateModel
    {
        public Guid Id {  get; set; }
        public string Status { get; set; }=default!;
    }
}
