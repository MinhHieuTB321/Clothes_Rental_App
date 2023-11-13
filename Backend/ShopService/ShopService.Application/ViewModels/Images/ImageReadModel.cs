using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.ViewModels.Images
{
    public class ImageReadModel
    {
        public Guid Id{get;set;}
        public Guid ProductId { get; set; }
        public string FileName { get; set; } = default!;
        public string FileUrl { get; set; } = default!;
    }
}
