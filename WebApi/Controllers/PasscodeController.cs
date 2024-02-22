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
    public class PasscodeController : ControllerBase
    {
        private readonly IRepository<Passcode> _repository;

        public PasscodeController(IRepository<Passcode> repository)
        {
            _repository = repository;
        }

        [HttpPost("CreateUpdatePasscode")]

        public Response CreateUpdatePasscode(Passcode obj)
        {
            Response response = new Response();
            LoginCredentials? credentials = null;

            try
            {
                 credentials = TokenManager.GetValidateToken(Request);

                if (credentials == null) return CustomStatusResponse.GetResponse(401);

                var Properties = new List<string> {"PasscodeID", "PasscodeValue", "StartTime", "EndTime", "TotalMinutes", "UsedMinutes", "ToolID","CredentialID", "IsActive", "ClientId" };

                var paremeters = DynamicParametersHelper.CreateParameters(obj,Properties, obj.PasscodeID > 0? "Update":"Create");

                var res = _repository.CRUD("ManagePasscode", paremeters);

                if (res == null) return CustomStatusResponse.GetResponse(320);
                else
                {
                    response = CustomStatusResponse.GetResponse(200);
                    response.Token =TokenManager.GenerateToken(credentials);
                    response.ResponseMsg = obj.PasscodeID > 0? "Passcode Update Successfully!" : "Passcode Create Successfully!";
                    response.Data = res;

                    return response;
                }
            }
            catch (DbException ex)
            {

                response = CustomStatusResponse.GetResponse(600);
                response.Token = TokenManager.GenerateToken(credentials);
                response.ResponseMsg = ex.Message;

                return response;
            }
            catch (Exception ex)
            {

                response = CustomStatusResponse.GetResponse(500);
                response.Token = TokenManager.GenerateToken(credentials);
                response.ResponseMsg = "Internal server error!";
                return response;
            }
        }


        [HttpPost("GetAllPasscode")]

        public Response GetAllPasscode()
        {
            Response response = new Response();

            LoginCredentials? credentials = null;

            try
            {
                 credentials = TokenManager.GetValidateToken(Request);
               
                if (credentials == null) return CustomStatusResponse.GetResponse(401);

                var user = _repository.GetAll("ManagePasscode");

                if (user == null) return CustomStatusResponse.GetResponse(320);
              
                else
                {
                    response = CustomStatusResponse.GetResponse(200);
                    response.Token = TokenManager.GenerateToken(credentials);
                    response.Data = user;

                    return response;
                }
            }
            catch (DbException ex)
            {

                response = CustomStatusResponse.GetResponse(600);
                response.ResponseMsg = ex.Message;
                response.Token = TokenManager.GenerateToken(credentials);
                return response;
            }
            catch (Exception ex)
            {

                response = CustomStatusResponse.GetResponse(500);
                response.ResponseMsg = "Internal server error!";
                response.Token = TokenManager.GenerateToken(credentials);
                return response;
            }
        }

        [HttpPost("GetPasscodeDetailsById/{Id}")]

        public Response GetPasscodeDetailsById(int Id)
        {
            Response response = new Response();
            LoginCredentials? credentials = null;

            try
            {
                credentials = TokenManager.GetValidateToken(Request);

                if (credentials == null) return CustomStatusResponse.GetResponse(401);

                var res = _repository.GetAll("ManagePasscode",Id, "PasscodeID");

                if (res == null) return CustomStatusResponse.GetResponse(320);
                else
                {
                    response = CustomStatusResponse.GetResponse(200);
                    response.Token = TokenManager.GenerateToken(credentials);
                    response.ResponseMsg = "";
                    response.Data = res;

                    return response;
                }
            }
            catch (DbException ex)
            {

                response = CustomStatusResponse.GetResponse(600);
                response.Token = TokenManager.GenerateToken(credentials);
                response.ResponseMsg = ex.Message;

                return response;
            }
            catch (Exception ex)
            {

                response = CustomStatusResponse.GetResponse(500);
                response.Token = TokenManager.GenerateToken(credentials);
                response.ResponseMsg = "Internal server error!";
                return response;
            }
        }

        


    }
}
