using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelClassLibrary;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using WebApi.IRepositories;
using WebApi.Service;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtensionController : ControllerBase
    {
        private readonly IRepository<ExtensionAuth> _repository;

        public ExtensionController(IRepository<ExtensionAuth> repository)
        {
            _repository = repository;
        }

        [HttpPost("ExtensionAuthantication")]

        public Response ExtensionAuthentication(ExtensionAuth obj)
        {
            Response response = new Response();
          
            try
            {

                var Properties = new List<string> {"ToolID", "PasscodeValue"};

                var paremeters = DynamicParametersHelper.CreateParameters(obj,Properties,"Read");

                var res = _repository.CRUD("ExtensionAuthentication", paremeters);

                if (res == null) return CustomStatusResponse.GetResponse(320);
                else
                {
                    response = CustomStatusResponse.GetResponse(200);
                    response.Token = "";
                    response.ResponseMsg = obj.CredentialID > 0? "Credentials Update Successfully!" : "Credentials Create Successfully!";
                    response.Data = res;

                    return response;
                }
            }
            catch (DbException ex)
            {

                response = CustomStatusResponse.GetResponse(600);
                response.Token = "";
                response.ResponseMsg = ex.Message;

                return response;
            }
            catch (Exception ex)
            {

                response = CustomStatusResponse.GetResponse(500);
                response.Token = "";
                response.ResponseMsg = "Internal server error!";
                return response;
            }
        }

        [HttpPost("ExtensionUpdateTime")]
        public Response ExtensionUpdateTime(UpdateUsedTime obj)
        {
            Response response = new Response();

            try
            {

                var paremeters = DynamicParametersHelper.CreateParameters(obj,null, "Update");

                var res = _repository.CRUD("ExtensionAuthentication", paremeters);

                if (res == null) return CustomStatusResponse.GetResponse(320);
                else
                {
                    response = CustomStatusResponse.GetResponse(200);
                    response.Token = "";
                    response.ResponseMsg = "Used Time Updated";
                    response.Data = res;

                    return response;
                }
            }
            catch (DbException ex)
            {

                response = CustomStatusResponse.GetResponse(600);
                response.Token = "";
                response.ResponseMsg = ex.Message;

                return response;
            }
            catch (Exception ex)
            {

                response = CustomStatusResponse.GetResponse(500);
                response.Token = "";
                response.ResponseMsg = "Internal server error!";
                return response;
            }
        }

    }
}
