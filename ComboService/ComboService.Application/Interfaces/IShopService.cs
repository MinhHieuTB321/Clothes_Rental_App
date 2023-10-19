using ComboService.Application.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.Interfaces
{
    public interface IShopService
    {
        public Task<IEnumerable<ShopResponseModel>> GetShops();
    }
}
