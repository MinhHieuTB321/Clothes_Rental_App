using ComboService.Application.ViewModels.PublishedModels;
using ComboService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.ViewModels.Response
{
    public class ComboResponseModel
    {
        public Guid Id { get; set; } = default!;
		public string ComboName { get; set; } = default!;
		public int Quantity { get; set; } = 0;
		public string Description { get; set; } = default!;
		public string FileName { get; set; } = default!;
		public string FileUrl { get; set; } = default!;
		public string Status { get; set; } = default!;
		public double TotalValue { get; set; } = 0;
		//Shop
		public Guid ShopId { get; set; } = default!;
        public string ShopName { get; set; }=default!;
        public ICollection<PriceListResponseModel> PriceList { get; set; }
        public ICollection<ProductComboResponseModel>? ProductCombos { get; set; }
    }

    public class ProductComboResponseModel
    {
		public Guid Id { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        //Combo
        public Guid ComboId { get; set; } = default!;
        public string ComboName { get; set; } = default!;
		public Guid ShopId { get; set; } = default!;
		public string ShopName { get; set; } = default!;
		//Product
		public Guid ProductId { get; set; } = default!;
		public string ProductName { get; set; } = default!;
		public string Description { get; set; } = default!;
		public string Status { get; set; } = default!;
		public int? Size { get; set; }
		public string? Color { get; set; }
		public string Material { get; set; } = default!;
		public double Price { get; set; } = default!;
		public double Compesation { get; set; } = default!;
		public Guid CategoryId { get; set; } = default!;
		public string CategoryName { get; set; } = default!;
		public ICollection<ImageReadModel> ProductImages { get; set; } = default!;

	}
}
