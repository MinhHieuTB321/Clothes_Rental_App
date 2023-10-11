using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.ViewModels.Images
{
    public class FileUploadModel
    {
        public string URL { get; set; } = default!;
        public string FileName { get; set; } = default!;
    }
}
