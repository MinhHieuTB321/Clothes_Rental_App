using ShopService.Application.Commons;
using ShopService.Application.ViewModels.Products;

namespace ShopService.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductReadModel> CreateProduct(ProductCreateModel productCreateModel);
        Task<bool> UpdateProduct(ProductUpdateModel productUpdateModel);
        Task<bool> DeleteProduct(Guid id);
        Task<Pagination<ProductReadModel>> GetAllAsync(int pageNumber = 0, int pageSize = 10);
        Task<ProductReadModel> GetByIdAsync(Guid id);

        Task<Pagination<ProductReadModel>> GetAllSubProductByRootId(Guid id,int pageNumber = 0, int pageSize = 10);
    }
}
