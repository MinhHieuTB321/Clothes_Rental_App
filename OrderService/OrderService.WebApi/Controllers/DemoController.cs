using Microsoft.AspNetCore.Mvc;

namespace OrderService.WebApi.Controllers
{
    public class DemoController:BaseController
    {
        [HttpGet]
        public IActionResult Get()
        {
            Console.WriteLine("Demo Docker");
            return Ok("Demo");
        }
    }
}
