using OrderService.Application.ViewModels.OrderDetails;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.ViewModels.Orders
{
    public class OrderReadModel
    {
        public Guid Id { get; set; }
        public double Total { get; set; }
        public string Note { get; set; } = default!;
        public DateTime CreationDate { get; set; }
        public string Status { get; set; } = OrderEnum.Pending.ToString()!;
        public Guid CustomerId { get; set; }
        public string CustomerPhone { get; set; } = default!;
        public string CustomerAddress { get; set; } = default!;
        public string CustomerName { get; set; }=default!;
        public Guid ShopId { get; set; }
        public string? ShopName { get; set; }
        public string ShopEmail { get; set; } = default!;
        public Guid OwnerId { get; set; }
        public string? ShopPhone { get; set; }
        public string? ShopAddress { get; set; }
        public List<OrderDetailReadModel>? OrderDetails { get; set; }
    }
}
