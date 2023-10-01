using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using UserService.Application.Interfaces;

namespace UserService.WebApi.Services
{
    public class ClaimsService:IClaimsService
    {
        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            // todo implementation to get the current userId
            var Id = httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId");
            GetCurrentUser = string.IsNullOrEmpty(Id) ? Guid.Empty : Guid.Parse(Id);

            var email = httpContextAccessor.HttpContext?.User.FindFirstValue("Email");
            GetEmail = email.IsNullOrEmpty() ? "" : email!.ToString();

        }
        public string GetEmail { get; }

        public Guid GetCurrentUser { get; }
    }
}