using ComboService.Application.ViewModels.Request;
using ComboService.Application.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.Interfaces
{
    public interface IComboService
    {

		Task<List<ProductComboResponseModel>> GetProductComboByComboId(Guid comboId);
        Task<IEnumerable<ComboResponseModel>> GetCombos(Guid? shopId);
        Task<ComboResponseModel> GetComboByGuid(Guid Id);
        Task<ComboResponseModel> DeleteCombo(Guid Id); 
        Task<ComboResponseModel> UpdateCombo(UpdateComboRequestModel request);
        Task<ComboResponseModel> CreateCombo(CreateComboRequestModel request);
        Task<List<ProductComboResponseModel>> AddProductCombo(Guid comboId, List<ProductComboRequestModel> request);
	}
}
