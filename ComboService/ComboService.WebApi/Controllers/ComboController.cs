using ComboService.Application.Interfaces;
using ComboService.Application.Services;
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
        private readonly IMessageBusClient _messageBusClient;

        public ComboController(IComboService service, IMessageBusClient messageBus)
        {
            _service = service;
            _messageBusClient = messageBus;
        }

        //public ComboController(IComboService comboService)
        //{
        //    _service=comboService;
        //}

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
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ComboResponseModel>> GetComboById(Guid id)
        {
            var rs = await _service.GetComboByGuid(id);
            return Ok(rs);
        }

        /// <summary>
        /// Create combo
        /// </summary>
        [Authorize(Roles = nameof(RoleEnum.Owner))]
        [HttpPost]
        public async Task<ActionResult<ComboResponseModel>> CreateCombo([FromForm] CreateComboRequestModel request)
        {
            var rs = await _service.CreateCombo(request);
            try
            {
                _messageBusClient.PublishedCombo(rs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asyncchronously: {ex.InnerException}");
            }
            return CreatedAtAction(nameof(GetComboById), new {id=rs.Id},rs);
        }

		/// <summary>
		/// Create combo
		/// </summary>
		[Authorize(Roles = nameof(RoleEnum.Owner))]
		[HttpPost("{id}/products-combo")]
		public async Task<ActionResult<ComboResponseModel>> CreateProductCombo(Guid id,List<ProductComboRequestModel> request)
		{
			var rs = await _service.AddProductCombo(id,request);
			try
			{
				_messageBusClient.PublishedCombo(await _service.GetComboByGuid(id));
			}
			catch (Exception ex)
			{
				Console.WriteLine($"--> Could not send asyncchronously: {ex.InnerException}");
			}
			return StatusCode(StatusCodes.Status201Created, rs);
		}

		/// <summary>
		/// Update combo
		/// </summary>
		[Authorize(Roles = nameof(RoleEnum.Owner))]
        [HttpPut("{id}")]
        public async Task<ActionResult<ComboResponseModel>> UpdateCombo(Guid id, [FromForm] UpdateComboRequestModel request)
        {
            var rs = await _service.UpdateCombo(id, request);
			try
			{
				_messageBusClient.UpdatedCombo(rs);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"--> Could not send asyncchronously: {ex.InnerException}");
			}
			return NoContent();
        }

        /// <summary>
        /// Delete Combo
        /// </summary>
        [Authorize(Roles = nameof(RoleEnum.Owner))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ComboResponseModel>> DeleteCombo(Guid id)
        {
            var rs = await _service.DeleteCombo(id);
			try
			{
				_messageBusClient.DeletedCombo(rs);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"--> Could not send asyncchronously: {ex.InnerException}");
			}
			return NoContent();
        }
    }
}
