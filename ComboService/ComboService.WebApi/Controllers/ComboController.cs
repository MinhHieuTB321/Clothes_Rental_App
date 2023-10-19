using ComboService.Application.Interfaces;
using ComboService.Application.ViewModels.Request;
using ComboService.Application.ViewModels.Response;
using ComboService.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComboService.WebApi.Controllers
{
    public class ComboController : BaseController
    {
        private readonly IComboService _service;

        public ComboController(IComboService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all combo
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComboResponseModel>>> GetAll()
        {
            var rs = await _service.GetCombos();
            return Ok(rs);
        }

        /// <summary>
        /// Get combo by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ComboResponseModel>> GetComboById(Guid Id)
        {
            var rs = await _service.GetComboByGuid(Id);
            return Ok(rs);
        }

        /// <summary>
        /// Create combo
        /// </summary>
        [Authorize(Roles = nameof(RoleEnum.Owner))]
        [HttpPost]
        public async Task<ActionResult<ComboResponseModel>> CreateCombo([FromBody] CreateComboRequestModel request)
        {
            var rs = await _service.CreateCombo(request);
            return Ok(rs);
        }

        /// <summary>
        /// Update combo
        /// </summary>
        [Authorize(Roles = nameof(RoleEnum.Owner))]
        [HttpPut("{id}")]
        public async Task<ActionResult<ComboResponseModel>> UpdateCombo(Guid id, [FromBody] UpdateComboRequestModel request)
        {
            var rs = await _service.UpdateCombo(id, request);
            return Ok(rs);
        }

        /// <summary>
        /// Delete Combo
        /// </summary>
        [Authorize(Roles = nameof(RoleEnum.Owner))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ComboResponseModel>> DeleteCombo(Guid id)
        {
            var rs = await _service.DeleteCombo(id);
            return Ok(rs);
        }
    }
}
