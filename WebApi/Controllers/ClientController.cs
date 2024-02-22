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
    public class ClientController : ControllerBase
    {
        private readonly IRepository<Client> _repository;

        public ClientController(IRepository<Client> repository)
        {
            _repository = repository;
        }


        [HttpPost("AddClient")]

        public Response AddClient(Client obj)
        {
            Response response = new Response();

            try
            {
                var Properties = new List<string> { "FirstName", "LastName", "Email","Password", "ContactNumber" };

                var paremeters = DynamicParametersHelper.CreateParameters(obj,Properties);

                var  res = _repository.CRUD("AddClient", paremeters);

                if (res == null) return CustomStatusResponse.GetResponse(320);
                else
                {
                    response = CustomStatusResponse.GetResponse(200);
                    response.Token = "";
                    response.ResponseMsg = "Registration Has Been Done ,Please Wating For Approval";
                    response.Data = res;

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

        [HttpPost("GetAllClients")]

        public Response GetAllClients()
        {
            Response response = new Response();
            LoginCredentials? Credentials = null;

            try
            {
                 Credentials = TokenManager.GetValidateToken(Request);

                if (Credentials == null) return CustomStatusResponse.GetResponse(401);

                var res = _repository.GetAllWithOutOperationType("GetAllClients");

                if (res == null) return CustomStatusResponse.GetResponse(320);
                else
                {
                    response = CustomStatusResponse.GetResponse(200);
                    response.Token = TokenManager.GenerateToken(Credentials);
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


        [HttpPost("ClientApproval/{Id}")]

        public Response ClientApproval(int Id)
        {
            Response response = new Response();
            LoginCredentials? Credentials = null;

            try
            {
                Credentials = TokenManager.GetValidateToken(Request);

                if (Credentials == null) return CustomStatusResponse.GetResponse(401);

                var res = _repository.GetAllByInteger("ClientApproval", Id);

                if (res == null) return CustomStatusResponse.GetResponse(320);
                else
                {
                    response = CustomStatusResponse.GetResponse(200);
                    response.Token = TokenManager.GenerateToken(Credentials);
                    response.ResponseMsg = "Approval Has Been Done";
                    response.Data = res;

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

        [HttpPost("ClientLogin")]

        public Response ClientLogin(Client obj)
        {
            Response response = new Response();
            

            try
            {
                var Properties = new List<string> {  "Email", "Password" };

                var paremeters = DynamicParametersHelper.CreateParameters(obj, Properties);

                var res = _repository.GETLIST("sp_ClientLogin", paremeters);

                if (res == null) return CustomStatusResponse.GetResponse(320);
                else
                {
                    response = CustomStatusResponse.GetResponse(200);
                    response.Token = "";
                    response.ResponseMsg = "Login Successfuly";
                    response.Data = res;

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
