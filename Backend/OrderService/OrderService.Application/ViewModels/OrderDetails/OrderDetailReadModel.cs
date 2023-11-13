using OrderService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.ViewModels.OrderDetails
{
    public class OrderDetailReadModel
    {
        public DateTime DueDate { get; set; }
        public double Deposit { get; set; } = default!;
        public double RentalPrice { get; set; } = default!;
        public string? Status { get; set; }
        public Guid OrderId { get; set; }
        public Guid ComboId { get; set; }
        public string? ComboName {  get; set; }

    }
}
