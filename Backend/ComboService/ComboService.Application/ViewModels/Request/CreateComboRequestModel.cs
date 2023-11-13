using ComboService.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace ComboService.Application.ViewModels.Request
{
    public class CreateComboRequestModel
    {
        public string ComboName { get; set; }
        public string Description{get;set;}
        public int Quantity { get; set; } 
        public string Status { get; set; } 
        public Guid ShopId { get; set; }
        public IFormFile File{get;set;}
        //public ICollection<ProductComboRequestModel> ProductCombos { get; set; } = default!;
    }

    public class ProductComboRequestModel
    {
        public Guid ComboId{get;set;}

        public int Quantity { get; set; } = default!;

        public Guid ProductId { get; set; }
    }
}
