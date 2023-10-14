using ComboService.Domain.Entities;
using ComboService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.ViewModels.ComboViewModel.Request
{
    public class CreateComboRequestModel
    {
        public string ComboName { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public string Status { get; set; } = nameof(ActiveEnum.Active);
        public decimal TotalValue { get; set; } = default!;
        public Guid ShopId { get; set; } = default!;
        public ICollection<ProductComboRequestModel> ProductCombos { get; set; } = default!;
    }

    public class ProductComboRequestModel
    {
        public int Quantity { get; set; } = default!;

        public Guid ProductId { get; set; } = default!;

    }
}
