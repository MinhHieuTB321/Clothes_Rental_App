using ComboService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.ViewModels.ComboViewModel.Request
{
    public class UpdateComboRequestModel
    {
        public Guid ComboId { get; set; } = default!;
        public string ComboName { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public string Status { get; set; } = nameof(ActiveEnum.Active);
    }
}
