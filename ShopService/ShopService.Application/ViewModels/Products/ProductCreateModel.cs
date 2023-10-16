using Microsoft.AspNetCore.Http;
using ShopService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.ViewModels.Products
{
    public class ProductCreateModel
    {
        public string ProductName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Status { get; set; } = default!;
        public int? Size { get; set; }
        public string? Color { get; set; }
        public string Material { get; set; } = default!;
        public double Price { get; set; } = default!;
        public double Compesation { get; set; } = default!;
        public Guid? RootProductId { get; set; } = default!;
        public Guid ShopId { get; set; } = default!;
        public Guid CategoryId { get; set; } = default!;
        public IEnumerable<IFormFile?> File { get; set; } = default!;
    }
}
