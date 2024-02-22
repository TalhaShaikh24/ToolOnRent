
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ModelClassLibrary;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    public class ClientController : Controller
    {
        private string BaseUrl = "";
        private readonly ILogger<DashboardController>?_logger;

        public ClientController(ILogger<ClientController> logger, IConfiguration configuration)
        {
            BaseUrl = configuration.GetSection("UrlSetting").GetSection("baseApiUrl").Value ?? "";
        }
        public IActionResult Index()
        {
            var Token = HttpContext.Session.GetString("authorization");

            if (String.IsNullOrEmpty(Token))
            {

                return RedirectToAction("Login", "Account");

            }
            return View();
        }

   
        [HttpPost]
        public Task<object> GetAllClients()
        {
            string content = "";

            return HttpUtility.CustomHttp(BaseUrl, "api/Client/GetAllClients", content, HttpContext);
        }

        [HttpPost]
        public Task<object> ClientApproval(int Id)
        {
            string content = "";

            return HttpUtility.CustomHttp(BaseUrl, "api/Client/ClientApproval/"+Id, content, HttpContext);
        }

        [HttpPost]
        public Task<object> AddClient(Client obj)
        {
            string content = JsonConvert.SerializeObject(obj);

            return HttpUtility.CustomHttp(BaseUrl, "api/Client/AddClient", content, HttpContext);
        }

   

    }
}
