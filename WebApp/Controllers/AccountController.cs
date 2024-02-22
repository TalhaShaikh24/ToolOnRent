using Microsoft.AspNetCore.Mvc;
using ModelClassLibrary;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private string BaseUrl = "";

        public AccountController(ILogger<AccountController> logger, IConfiguration configuration)
        {
            _logger = logger;
            BaseUrl = configuration.GetSection("UrlSetting").GetSection("baseApiUrl").Value??"";

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public Task<object> Authentication([FromBody] LoginCredentials obj)
        {
            string content = JsonConvert.SerializeObject(obj);

            return HttpUtility.CustomHttp(BaseUrl, "api/Account/Authentication", content, HttpContext);
        }

    }
}
