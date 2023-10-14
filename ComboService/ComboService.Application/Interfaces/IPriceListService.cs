using ComboService.Application.ViewModels.ComboViewModel.Request;
using ComboService.Application.ViewModels.ComboViewModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.Interfaces
{
    public interface IPriceListService
    {
        Task<IEnumerable<PriceListResponseModel>> GetPriceLists();
        Task<PriceListResponseModel> GetPriceListByGuid(Guid Id);
        Task<PriceListResponseModel> DeletePriceList(Guid Id);
        Task<PriceListResponseModel> UpdatePriceList(Guid Id, UpdatePriceListRequestModel request);
        Task<PriceListResponseModel> CreatePriceList(CreatePriceListRequestModel request);
    }
}
