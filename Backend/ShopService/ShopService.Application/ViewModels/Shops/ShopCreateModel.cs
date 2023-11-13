using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.ViewModels.Shops
{
    public class ShopCreateModel
    {
        public string ShopName { get; set; } = default!;
        public string ShopEmail { get; set; } = default!;
        public string ShopPhone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Status { get; set; } = default!;

        public Guid OwnerId { get; set; } = default!;

        public IFormFile File { get; set; } = default!;
    }
}
