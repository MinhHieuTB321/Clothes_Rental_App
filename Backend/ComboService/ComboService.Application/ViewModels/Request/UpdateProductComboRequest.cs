using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.ViewModels.Request
{
	public class UpdateProductComboRequest
	{
		public Guid Id { get; set; }
		public int Quantity {  get; set; }
	}
}
