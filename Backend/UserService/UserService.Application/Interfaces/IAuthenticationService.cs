using UserService.Application.ViewModels.Authentications;

namespace UserService.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponseModel> LoginAsync(AuthenticationRequestModel model);
    }
    
}