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
        Task<IEnumerable<ComboResponseModel>> GetCombos();
        Task<ComboResponseModel> GetComboByGuid(Guid Id);
        Task<ComboResponseModel> DeleteCombo(Guid Id); 
        Task<ComboResponseModel> UpdateCombo(Guid Id, UpdateComboRequestModel request);
        Task<ComboResponseModel> CreateCombo(CreateComboRequestModel request);
    }
}
