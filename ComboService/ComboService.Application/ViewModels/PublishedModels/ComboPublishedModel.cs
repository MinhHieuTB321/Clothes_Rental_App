using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.ViewModels.PublishedModels
{
	public class ComboPublishedModel
	{
		public Guid Id { get; set; }
		public string ComboName { get; set; } = default!;
		public int Quantity { get; set; } = default!;
		public string Status { get; set; } = default!;
		public double TotalValue { get; set; } = default!;
		public Guid ShopId { get; set; } = default!;
		public string Event { get; set; } = "Combo_Published";
	}
}
