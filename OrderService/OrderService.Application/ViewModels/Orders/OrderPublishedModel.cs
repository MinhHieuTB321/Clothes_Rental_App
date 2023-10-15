﻿using OrderService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.ViewModels.Orders
{
    public class OrderPublishedModel
    {
        public Guid Id { get; set; }
        public double Total { get; set; }
        public string? Status { get; set; }
        public Guid CustomerId { get; set; }
        public Guid OwnerId { get; set; }
        public string? Event { get; set; }
    }
}
