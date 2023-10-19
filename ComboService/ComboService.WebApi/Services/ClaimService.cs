using ComboService.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ComboService.WebApi.Services
{
    public class ClaimService : IClaimService
    {
        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            // to get the current userId
            var Id = httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId");
            GetCurrentUser = string.IsNullOrEmpty(Id) ? Guid.Empty : Guid.Parse(Id);

            var email = httpContextAccessor.HttpContext?.User.FindFirstValue("Email");
            GetEmail = email.IsNullOrEmpty() ? "" : email!.ToString();
        }

        public Guid GetCurrentUser { get; }

        public string GetEmail { get; }
    }
}
