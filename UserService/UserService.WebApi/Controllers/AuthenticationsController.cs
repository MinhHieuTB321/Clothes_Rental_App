
using Azure.Core;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using UserService.Application.Interfaces;
using UserService.Application.ViewModels.Authentications;

namespace UserService.WebApi.Controllers
{
    public class AuthenticationsController:BaseController
    {
        private readonly IAuthenticationService _service;
        public AuthenticationsController(IAuthenticationService service)
        {
            _service = service;
        }
        //private static readonly HttpClient _httpClient = new HttpClient();

      
        [HttpPost]
        public async Task<IActionResult> LoginAsync(AuthenticationRequestModel model)
        {
            var token = await _service.LoginAsync(model);
            return Ok(token);
        }

        //[HttpPost]
        //public async Task<IActionResult> LoginAsync(string model)
        //{
        //    var clientId = "1053732993433-djk7r11upd9qnmktfk1gg9pkj7r8h8je.apps.googleusercontent.com";
        //    var clientSecret = "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";

        //    using (var httpClient = new HttpClient())
        //    {
        //        var basicAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
        //        //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basicAuth);
        //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", model);
        //        //httpClient.DefaultRequestHeaders.Add("X-Client-Id", clientId);
        //        var response = await httpClient.GetAsync("https://www.googleapis.com/oauth2/v1/userinfo");

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var userInfo = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine(basicAuth);
        //            Console.WriteLine(userInfo);
        //        }
        //        else
        //        {
        //            Console.WriteLine("Error getting user info");
        //            Console.WriteLine(response);
        //        }
        //    }
        //    return Ok();
        //}

        [HttpGet]
        public IActionResult Get(){
            Console.WriteLine("Demo Docler");
            return Ok("Demo");
        }
    }
}
