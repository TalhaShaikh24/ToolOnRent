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
    public class CredentialController : ControllerBase
    {
        private readonly IRepository<Credentials> _repository;

        public CredentialController(IRepository<Credentials> repository)
        {
            _repository = repository;
        }

        [HttpPost("CreateUpdateCredentials")]

        public Response CreateUpdateCredentials(Credentials obj)
        {
            Response response = new Response();
            LoginCredentials? Credentials = null;

            try
            {
                Credentials = TokenManager.GetValidateToken(Request);

                if (Credentials == null) return CustomStatusResponse.GetResponse(401);

                var Properties = new List<string> {"CredentialID", "ToolID", "Email", "Password"};

                var paremeters = DynamicParametersHelper.CreateParameters(obj,Properties, obj.CredentialID > 0? "Update":"Create");

                var res = _repository.CRUD("ManageCredential", paremeters);

                if (res == null) return CustomStatusResponse.GetResponse(320);
                else
                {
                    response = CustomStatusResponse.GetResponse(200);
                    response.Token =TokenManager.GenerateToken(Credentials);
                    response.ResponseMsg = obj.CredentialID > 0? "Credentials Update Successfully!" : "Credentials Create Successfully!";
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


        [HttpPost("GetAllCredential")]

        public Response GetAllCredential()
        {
            Response response = new Response();

            LoginCredentials? Credentials = null;

            try
            {
                 Credentials = TokenManager.GetValidateToken(Request);
               
                if (Credentials == null) return CustomStatusResponse.GetResponse(401);

                var user = _repository.GetAll("ManageCredential");

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
        
  

        [HttpPost("GetCredentialDetailsById/{Id}")]

        public Response GetCredentialDetailsById(int Id)
        {
            Response response = new Response();
            LoginCredentials? Credentials = null;

            try
            {
                Credentials = TokenManager.GetValidateToken(Request);

                if (Credentials == null) return CustomStatusResponse.GetResponse(401);

                var res = _repository.GetAll("ManageCredential",Id, "CredentialID");

                if (res == null) return CustomStatusResponse.GetResponse(320);
                else
                {
                    response = CustomStatusResponse.GetResponse(200);
                    response.Token = TokenManager.GenerateToken(Credentials);
                    response.ResponseMsg = "";
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



        [HttpPost("GetToolEmailById/{Id}")]

        public Response GetToolEmailById(int Id)
        {
            Response response = new Response();
            LoginCredentials? credentials = null;

            try
            {
                credentials = TokenManager.GetValidateToken(Request);

                if (credentials == null) return CustomStatusResponse.GetResponse(401);

                var res = _repository.GetAllByInteger("GetToolsByEmail", Id, "Id");

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
