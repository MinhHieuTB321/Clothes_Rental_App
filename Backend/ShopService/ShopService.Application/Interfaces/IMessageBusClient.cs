using ShopService.Application.ViewModels.Owners;
using ShopService.Application.ViewModels.Products;
using ShopService.Application.ViewModels.Shops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.Interfaces
{
    public interface IMessageBusClient
    {
        void PublishedNewShop(ShopReadModel model);

        void UpdatedShop(ShopReadModel model);

        void DeletedShop(ShopReadModel model);
        void PublishedNewProduct(ProductReadModel model);

        void UpdatedProduct(ProductReadModel model);

        void DeletedProduct(ProductReadModel model);
    }
}
