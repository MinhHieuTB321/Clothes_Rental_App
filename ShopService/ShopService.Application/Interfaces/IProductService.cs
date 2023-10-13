using ShopService.Application.ViewModels.Products;
using ShopService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.Interfaces
{
    public interface IProductService
    {
        Task<Product> CreateProduct(ProductCreateModel productCreateModel);
        Task<ProductReadModel> UpdateProduct(ProductUpdateModel productUpdateModel);
        Task<bool> DeleteProduct(Guid id);
        Task<IEnumerable<ProductReadModel>> GetAllAsync();
        Task<ProductReadModel> GetByIdAsync(Guid id);
    }
}
