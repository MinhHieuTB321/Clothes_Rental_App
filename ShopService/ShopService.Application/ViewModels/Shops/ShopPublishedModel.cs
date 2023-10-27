using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.ViewModels.Shops
{
    public class ShopPublishedModel
    {
        public Guid Id { get; set; }
        public string ShopName { get; set; } = default!;
        public string ShopCode { get; set; } = default!;
        public string ShopEmail { get; set; } = default!;
        public string ShopPhone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string FileName { get; set; } = "ShopLogo";
        public string FileUrl { get; set; } = default!;
        public Guid OwnerId { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string? Event { get; set; } = "Shop_Published";
    }
}
