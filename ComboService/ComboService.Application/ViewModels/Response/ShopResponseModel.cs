using ComboService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.ViewModels.Response
{
    public class ShopResponseModel
    {
        public Guid Id { get; set; } = default!;
        public Guid OwnerId { get; set; } = default!;
        public string ShopName { get; set; } = default!;
        public string ShopCode { get; set; } = default!;
        public string ShopEmail { get; set; } = default!;
        public string ShopPhone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Status { get; set; } = default!;

        public ICollection<string> Logo { get; set; } = default!;

        public ICollection<Combo> Combos { get; set; } = default!;
    }
}
