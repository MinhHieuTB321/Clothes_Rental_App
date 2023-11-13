using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Interfaces;
using UserService.Application.ViewModels.Wallets;

namespace UserService.WebApi.Controllers
{
    public class WalletsController:BaseController
    {
        private readonly IWalletService _service;
        public WalletsController(IWalletService service)
        {
            _service = service;
        }
        /// <summary>
        /// Get Wallet By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWalletById(Guid id)
        {
            var result = await _service.GetWalletbyId(id);
            return Ok(result);
        }


        /// <summary>
        /// Update Wallet
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateWallet(WalletUpdateModel model)
        {
            await _service.UpdateWallet(model);
            return NoContent();
        }
    }
}
