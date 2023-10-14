using ComboService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.ViewModels.ComboViewModel.Response
{
    public class ComboResponseModel
    {
        public Guid Id { get; set; } = default!;
        public string ComboName { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public string Status { get; set; } = default!;
        public decimal TotalValue { get; set; } = default!;
        //Shop
        public Guid ShopId { get; set; } = default!;

        public ICollection<ProductComboResponseModel> ProductCombos { get; set; }
    }

    public class ProductComboResponseModel
    {
        public int Quantity { get; set; } = default!;
        //Combo
        public Guid ComboId { get; set; } = default!;

        //Product
        public Guid ProductId { get; set; } = default!;

    }
}
