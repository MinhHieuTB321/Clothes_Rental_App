using ComboService.Application.ViewModels.Request;
using ComboService.Application.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.Interfaces
{
    public interface IPriceListService
    {
        Task<IEnumerable<PriceListResponseModel>> GetPriceLists(Guid comboId);
        Task<PriceListResponseModel> GetPriceListByGuid(Guid Id);
        Task<PriceListResponseModel> DeletePriceList(Guid Id);
        Task<PriceListResponseModel> UpdatePriceList(Guid Id, UpdatePriceListRequestModel request);
        Task<PriceListResponseModel> CreatePriceList(CreatePriceListRequestModel request);
    }
}
