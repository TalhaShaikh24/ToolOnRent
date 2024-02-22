
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ModelClassLibrary;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    public class DashboardController : Controller
    {
        private string BaseUrl = "";
        private readonly ILogger<DashboardController>?_logger;

        public DashboardController(ILogger<DashboardController> logger, IConfiguration configuration)
        {
            BaseUrl = configuration.GetSection("UrlSetting").GetSection("baseApiUrl").Value ?? "";
        }
        public IActionResult Dashboard()
        {
            var Token = HttpContext.Session.GetString("authorization");

            if (String.IsNullOrEmpty(Token))
            {

                return RedirectToAction("Login", "Account");

            }
            return View();
        }

        public IActionResult Test()
        {


           

            return View();
        }



        public IActionResult Tools()
        {
            var Token = HttpContext.Session.GetString("authorization");

            if (String.IsNullOrEmpty(Token))
            {

                return RedirectToAction("Login", "Account");

            }
            return View();
        }

        public IActionResult Credential()
        {
            var Token = HttpContext.Session.GetString("authorization");

            if (String.IsNullOrEmpty(Token))
            {

                return RedirectToAction("Login", "Account");

            }
            return View();
        }
        public IActionResult Passcode()
        {
            var Token = HttpContext.Session.GetString("authorization");

            if (String.IsNullOrEmpty(Token))
            {

                return RedirectToAction("Login", "Account");

            }
            return View();
        }

        #region ManageTool
        [HttpPost]
        public Task<object> CreateUpdateTool([FromBody]  Tools obj)
        {
            string content = JsonConvert.SerializeObject(obj);

            return HttpUtility.CustomHttp(BaseUrl, "api/Dashboard/CreateUpdateTool", content, HttpContext);
        }

        [HttpPost]
        public Task<object> GetAllTool()
        {
            string content = "";

            return HttpUtility.CustomHttp(BaseUrl, "api/Dashboard/GetAllTool", content, HttpContext);
        }

        [HttpPost]
        public Task<object> GetToolDetailsById(int Id)
        {
            string content = "";

            return HttpUtility.CustomHttp(BaseUrl, "api/Dashboard/GetToolDetailsById/"+Id, content, HttpContext);
        }

        

        #endregion

        #region ManageCredential
        [HttpPost]
        public Task<object> CreateUpdateCredentials([FromBody] Credentials obj)
        {
            string content = JsonConvert.SerializeObject(obj);

            return HttpUtility.CustomHttp(BaseUrl, "api/Credential/CreateUpdateCredentials", content, HttpContext);
        }

        [HttpPost]
        public Task<object> GetAllCredential()
        {
            string content = "";

            return HttpUtility.CustomHttp(BaseUrl, "api/Credential/GetAllCredential", content, HttpContext);
        }

        

        [HttpPost]
        public Task<object> GetCredentialDetailsById(int Id)
        {
            string content = "";

            return HttpUtility.CustomHttp(BaseUrl, "api/Credential/GetCredentialDetailsById/" + Id, content, HttpContext);
        }

        [HttpPost]
        public Task<object> GetToolEmailById(int Id)
        {
            string content = "";

            return HttpUtility.CustomHttp(BaseUrl, "api/Credential/GetToolEmailById/" + Id, content, HttpContext);
        }

        #endregion

        #region ManagePasscode
        [HttpPost]
        public Task<object> CreateUpdatePasscode([FromBody] Passcode obj)
        {
            string content = JsonConvert.SerializeObject(obj);

            return HttpUtility.CustomHttp(BaseUrl, "api/Passcode/CreateUpdatePasscode", content, HttpContext);
        }

        [HttpPost]
        public Task<object> GetAllPasscode()
        {
            string content = "";

            return HttpUtility.CustomHttp(BaseUrl, "api/Passcode/GetAllPasscode", content, HttpContext);
        }

        [HttpPost]
        public Task<object> GetPasscodeDetailsById(int Id)
        {
            string content = "";

            return HttpUtility.CustomHttp(BaseUrl, "api/Passcode/GetPasscodeDetailsById/" + Id, content, HttpContext);
        }

        [HttpPost]
        public Task<object> GetToolsByEmail(string Email)
        {
            string content = "";

            return HttpUtility.CustomHttp(BaseUrl,"api/Dashboard/GetToolsByEmail/"+Email, content, HttpContext);
        }

        #endregion
    }
}
