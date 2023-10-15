using OrderService.Application.ViewModels.OrderDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.ViewModels.Orders
{
    public class OrderCreateModel
    { 
        public Guid ShopId { get; set; }
        public string Note { get; set; } = default!;

        public List<OrderDetailCreateModel> OrderDetails { get; set; } = default!;   
    }
}
 