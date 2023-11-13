using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComboService.Application.ViewModels.Request;
using ComboService.Application.ViewModels.Response;

namespace ComboService.Application.Interfaces
{
    public interface IProductComboService
    {
        Task<List<ProductComboResponseModel>> Create(List<ProductComboRequestModel> request);
        Task <bool> Update(UpdateProductComboRequest request);
		Task<List<ProductComboResponseModel>> GetAllByComboId(Guid comboId);

        Task<List<ProductResponseModel>> GetNonProductByComboId(Guid comboId,Guid shopId);

		Task<ProductComboResponseModel> GetProductComboById(Guid id);
		Task<bool> Delete(Guid id);
	}
}
