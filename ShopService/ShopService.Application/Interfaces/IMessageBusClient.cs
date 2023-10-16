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
        void PublishedNewOwner(OwnerReadModel model);
        void PublishedNewShop(ShopReadModel model);
        void PublishedNewProduct(ProductReadModel model);
    }
}
