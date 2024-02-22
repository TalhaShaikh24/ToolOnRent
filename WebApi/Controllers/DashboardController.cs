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
    public class DashboardController : ControllerBase
    {
        private readonly IRepository<Tools> _repository;

        public DashboardController(IRepository<Tools> repository)
        {
            _repository = repository;
        }

        [HttpPost("CreateUpdateTool")]

        public Response CreateUpdateTool(Tools obj)
        {
            Response response = new Response();
            LoginCredentials? Credentials = null;

            try
            {
                Credentials = TokenManager.GetValidateToken(Request);

                if (Credentials == null) return CustomStatusResponse.GetResponse(401);

                var paremeters = DynamicParametersHelper.CreateParameters(obj,null, obj.ToolID > 0? "Update":"Create");

                var res = _repository.CRUD("ManageTool", paremeters);

                if (res == null) return CustomStatusResponse.GetResponse(320);
                else
                {
                    response = CustomStatusResponse.GetResponse(200);
                    response.Token =TokenManager.GenerateToken(Credentials);
                    response.ResponseMsg = obj.ToolID > 0? "Tool Update Successfully!" : "Tool Create Successfully!";
                    response.Data = res;

                    return response;
                }
            }
            catch (DbException ex)
            {

                response = CustomStatusResponse.GetResponse(600);
                response.Token = TokenManager.GenerateToken(Credentials);
                response.ResponseMsg = ex.Message;

                return response;
            }
            catch (Exception ex)
            {

                response = CustomStatusResponse.GetResponse(500);
                response.Token = TokenManager.GenerateToken(Credentials);
                response.ResponseMsg = "Internal server error!";
                return response;
            }
        }


        [HttpPost("GetAllTool")]

        public Response GetAllTool()
        {
            Response response = new Response();

            LoginCredentials? Credentials = null;

            try
            {
                Credentials = TokenManager.GetValidateToken(Request);

                if (Credentials == null) return CustomStatusResponse.GetResponse(401);

                var user = _repository.GetAll("ManageTool");

                if (user == null) return CustomStatusResponse.GetResponse(320);
                else
                {
                    response = CustomStatusResponse.GetResponse(200);
                    response.Token = TokenManager.GenerateToken(Credentials);
                    response.Data = user;

                    return response;
                }
            }
            catch (DbException ex)
            {

                response = CustomStatusResponse.GetResponse(600);
                response.ResponseMsg = ex.Message;
                response.Token = TokenManager.GenerateToken(Credentials);
                return response;
            }
            catch (Exception ex)
            {

                response = CustomStatusResponse.GetResponse(500);
                response.ResponseMsg = "Internal server error!";
                response.Token = TokenManager.GenerateToken(Credentials);
                return response;
            }
        }

        [HttpPost("GetToolDetailsById/{Id}")]

        public Response GetToolDetailsById(int Id)
        {
            Response response = new Response();
            LoginCredentials? Credentials = null;

            try
            {
                Credentials = TokenManager.GetValidateToken(Request);

                if (Credentials == null) return CustomStatusResponse.GetResponse(401);

                var res = _repository.GetAll("ManageTool",Id,"ToolID");

                if (res == null) return CustomStatusResponse.GetResponse(320);
                else
                {
                    response = CustomStatusResponse.GetResponse(200);
                    response.Token = TokenManager.GenerateToken(Credentials);
                    response.ResponseMsg = "Tool Create Successfully!";
                    response.Data = res;

                    return response;
                }
            }
            catch (DbException ex)
            {

                response = CustomStatusResponse.GetResponse(600);
                response.Token = TokenManager.GenerateToken(Credentials);
                response.ResponseMsg = ex.Message;

                return response;
            }
            catch (Exception ex)
            {

                response = CustomStatusResponse.GetResponse(500);
                response.Token = TokenManager.GenerateToken(Credentials);
                response.ResponseMsg = "Internal server error!";
                return response;
            }
        }

    }
}
