namespace  UserService.Application.ViewModels.Authentications
{
    public class AuthenticationResponseModel
    {
        public Guid Id{get;set;}
        public string? Name{get;set;}
        public string? Role{get;set;}
        public string? AccessToken {get;set;}

    }
}