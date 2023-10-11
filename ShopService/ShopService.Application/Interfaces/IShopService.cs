using ShopService.Application.ViewModels.Shops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.Interfaces
{
    public interface IShopService
    {
        Task<ShopReadModel> CreateShop(ShopCreateModel shopCreateModel);
        Task<ShopReadModel> UpdateShop(ShopUpdateModel shopUpdateModel);
        Task<bool> DeleteShop(Guid shopId);
        Task<IEnumerable<ShopReadModel>> GetAllAsync();
        Task<ShopReadModel> GetByIdAsync(Guid id);
    }
}
