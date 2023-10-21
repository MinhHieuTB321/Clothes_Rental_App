using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.ViewModels.Customers
{
    public class CustomerPublishedModel
    {
      public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Gender{get;set;}=default!;
        public string Address{get;set;}=default!;
        public string Status{get;set;}="Active";
    }
}
