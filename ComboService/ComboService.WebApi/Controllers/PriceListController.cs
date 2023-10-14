using ComboService.Application.Interfaces;
using ComboService.Application.ViewModels.ComboViewModel.Request;
using ComboService.Application.ViewModels.ComboViewModel.Response;
using Microsoft.AspNetCore.Mvc;

namespace ComboService.WebApi.Controllers
{
    public class PriceListController : BaseController
    {
        private readonly IPriceListService _service;

        public PriceListController(IPriceListService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all pricelist
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriceListResponseModel>>> GetPriceLists()
        {
            var rs = await _service.GetPriceLists();
            return Ok(rs);
        }

        /// <summary>
        /// Get pricelist
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PriceListResponseModel>> GetPriceList(Guid id)
        {
            var rs = await _service.GetPriceListByGuid(id);
            return Ok(rs);
        }

        /// <summary>
        /// Create priceList
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<PriceListResponseModel>>> CreatePriceList([FromBody] CreatePriceListRequestModel request)
        {
            var rs = await _service.CreatePriceList(request);
            return Ok(rs);
        }

        /// <summary>
        /// Update pricelisst
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<PriceListResponseModel>> UpdatePriceList(Guid id, [FromBody] UpdatePriceListRequestModel request)
        {
            var rs = await _service.UpdatePriceList(id, request);
            return Ok(rs);
        }

        /// <summary>
        /// Delete Price
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<PriceListResponseModel>> DeleteCombo(Guid id)
        {
            var rs = await _service.DeletePriceList(id);
            return Ok(rs);
        }
    }
}
