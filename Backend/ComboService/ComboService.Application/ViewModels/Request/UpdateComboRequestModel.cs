using Microsoft.AspNetCore.Http;

namespace ComboService.Application.ViewModels.Request
{
    public class UpdateComboRequestModel
    {
        public Guid Id { get; set; } = default!;
        public string ComboName { get; set; } = default!;
        public string Description{get;set;}=default!;
        public int Quantity { get; set; } = default!;
        public IFormFile? File{get;set;}=default!;
    }
}
