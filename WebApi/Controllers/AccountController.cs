using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelClassLibrary;
using System.Data;
using System.Data.Common;
using System.Reflection;
using WebApi.IRepositories;
using WebApi.Service;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IRepository<LoginCredentials>? _repository;

        public AccountController(IRepository<LoginCredentials> repository)
        {
            _repository = repository;
        }

        [HttpPost("Authentication")]

        public Response Authentication(LoginCredentials obj)
        {
            Response response = new Response();

            try
            {
                var Properties = new List<string> {"Email", "Password"};

                var paremeters = DynamicParametersHelper.CreateParameters(obj, Properties);

                var user = _repository.CRUD("sp_Login", paremeters);

                if (user == null) return CustomStatusResponse.GetResponse(320);
                else
                {
                    response = CustomStatusResponse.GetResponse(200);
                    response.Token =TokenManager.GenerateToken(user);
                    response.Data = user;

                    return response;
                }
            }
            catch (DbException ex)
            {

                response = CustomStatusResponse.GetResponse(600);
                response.Token = null;
                response.ResponseMsg = ex.Message;

                return response;
            }
            catch (Exception ex)
            {

                response = CustomStatusResponse.GetResponse(500);
                response.Token = null;
                response.ResponseMsg = "Internal server error!";
                return response;
            }
        }

        [HttpPost("Registration")]

        public Response Registration(LoginCredentials obj)
        {
            Response response = new Response();

            try
            {
                var paremeters = DynamicParametersHelper.CreateParameters(obj);

                var user = _repository.CRUD("Sp_Registration", paremeters);

                if (user == null) return CustomStatusResponse.GetResponse(320);
                else
                {
                    response = CustomStatusResponse.GetResponse(200);
                    response.Token = "";
                    response.Data = user;

                    return response;
                }
            }
            catch (DbException ex)
            {

                response = CustomStatusResponse.GetResponse(600);

                response.ResponseMsg = ex.Message;

                return response;
            }
            catch (Exception ex)
            {

                response = CustomStatusResponse.GetResponse(500);
                response.ResponseMsg = "Internal server error!";
                return response;
            }
        }

        [HttpPost("GetAllRegistration")]

        public Response GetAllRegistration()
        {
            Response response = new Response();

            try
            {
                var user = _repository.GetAll("Sp_GetAllRegistration");

                if (user == null) return CustomStatusResponse.GetResponse(320);
                else
                {
                    response = CustomStatusResponse.GetResponse(200);
                    response.Token = "";
                    response.Data = user;

                    return response;
                }
            }
            catch (DbException ex)
            {

                response = CustomStatusResponse.GetResponse(600);

                response.ResponseMsg = ex.Message;

                return response;
            }
            catch (Exception ex)
            {

                response = CustomStatusResponse.GetResponse(500);
                response.ResponseMsg = "Internal server error!";
                return response;
            }
        }
    }
}
