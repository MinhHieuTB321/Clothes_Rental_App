using Microsoft.AspNetCore.Http;

namespace ShopService.Application.ViewModels.Products
{
    public class ProductUpdateModel
    {
        public Guid Id { get; set; }
         public string ProductName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int? Size { get; set; }
        public string? Color { get; set; }
        public string Material { get; set; } = default!;
        public double Price { get; set; } = default!;
        public double Compesation { get; set; } = default!;
        public Guid CategoryId { get; set; } = default!;
        //public IEnumerable<IFormFile>? File { get; set; } = default!;
    }
}
