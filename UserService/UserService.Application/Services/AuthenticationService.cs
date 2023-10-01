using Microsoft.Extensions.Configuration;
using UserService.Application.GlobalExceptionHandling.Exceptions;
using UserService.Application.Interfaces;
using UserService.Application.Utils;
using UserService.Application.ViewModels.Authentications;

namespace UserService.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        
        public AuthenticationService(IUnitOfWork unitOfWork,IConfiguration configuration)
        {
            _unitOfWork=unitOfWork;
            _config=configuration;
        }
        public async Task<AuthenticationResponseModel> LoginAsync(AuthenticationRequestModel model)
        {
            var user= await _unitOfWork.UserRepository.FindByField(x=>x.Email==model.Email);
            if(user==null) throw new NotFoundException("User is not exist!");
            
            var accessToken= user.GenerateJsonWebToken(_config["JWTSecretKey"]!);
            return new AuthenticationResponseModel{AccessToken=accessToken};
        }
    }
}