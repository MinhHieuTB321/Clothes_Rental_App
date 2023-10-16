using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.ViewModels.Categories
{
    public class CategoryReadModel
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
