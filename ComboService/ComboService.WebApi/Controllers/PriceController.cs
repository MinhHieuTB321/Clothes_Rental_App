using ComboService.Application.Interfaces;
using ComboService.Application.ViewModels.Request;
using ComboService.Application.ViewModels.Response;
using ComboService.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComboService.WebApi.Controllers
{
    public class PriceController : BaseController
    {
        private readonly IPriceListService _service;

        public PriceController(IPriceListService service)
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
		public async Task<ActionResult<PriceListResponseModel>> GetPriceListById(Guid id)
        {
            var rs = await _service.GetPriceListByGuid(id);
            return Ok(rs);
        }

        /// <summary>
        /// Create priceList
        /// </summary>
        [Authorize(Roles = nameof(RoleEnum.Owner))]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<PriceListResponseModel>>> CreatePriceList([FromBody] CreatePriceListRequestModel request)
        {
            var rs = await _service.CreatePriceList(request);
            return CreatedAtAction(nameof(GetPriceListById),new {id=rs.Id},rs);
        }

        /// <summary>
        /// Update pricelisst
        /// </summary>
        [Authorize(Roles = nameof(RoleEnum.Owner))]
        [HttpPut("{id}")]
        public async Task<ActionResult<PriceListResponseModel>> UpdatePriceList(Guid id, [FromBody] UpdatePriceListRequestModel request)
        {
            var rs = await _service.UpdatePriceList(id, request);
            return NoContent();
        }

        /// <summary>
        /// Delete Price
        /// </summary>
        [Authorize(Roles = nameof(RoleEnum.Owner))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<PriceListResponseModel>> DeletePrice(Guid id)
        {
            var rs = await _service.DeletePriceList(id);
            return NoContent();
        }
    }
}
