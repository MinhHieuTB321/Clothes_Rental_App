using ComboService.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace ComboService.Application.ViewModels.Request
{
    public class CreateComboRequestModel
    {
        public string ComboName { get; set; } = default!;
        public string Description{get;set;}=default!;
        public string Status { get; set; } = nameof(ActiveEnum.Active);
        public Guid ShopId { get; set; } = default!;
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
