using ShopService.Application.ViewModels.Owners;
using ShopService.Application.ViewModels.Shops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.Interfaces
{
    public interface IOwnerService
    {
        Task<OwnerReadModel> CreteOwner(OwnerCreateModel ownerCreateModel);
        Task<OwnerReadModel> UpdateOwner(OwnerUpdateModel ownerUpdateModel);
        Task<IEnumerable<OwnerReadModel>> GetAllAsync();
        Task<OwnerReadModel> GetByIdAsync(Guid id);
        Task<bool> DeleteOwner(Guid id);

        Task<List<ShopReadModel>>GetAllShopByOwnerId(Guid ownerId);

    }
}
