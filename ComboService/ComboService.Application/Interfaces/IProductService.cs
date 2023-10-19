using ComboService.Application.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductResponseModel>> GetProducts();
    }
}
