using ShopService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.ViewModels.Owners
{
    public class OwnerPublishedModel
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
