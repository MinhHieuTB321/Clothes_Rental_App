using ShopService.Application.ViewModels.Categories;
using ShopService.Application.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryReadModel> CreateCategory(CategoryCreateModel categoryCreateModel);
        Task<CategoryReadModel> UpdateCategory(CategoryUpdateModel categoryUpdateModel);
        Task<IEnumerable<CategoryReadModel>> GetAllAsync();
        Task<CategoryReadModel> GetByIdAsync(Guid id);
        Task<bool> DeleteCategory(Guid id);

        Task<List<ProductReadModel>> GetAllProductByCateId(Guid id);
    }
}
