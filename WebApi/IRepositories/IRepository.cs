using Dapper;
using Microsoft.OpenApi.Models;

namespace WebApi.IRepositories
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll(string Sp_Name,int Id=0,string EnitityType="Id", string OperationType= "Read");
        List<T> GetAllWithOutOperationType(string Sp_Name);
        List<T> GetAllByString(string Sp_Name, string Id = "", string EnitityType = "Id", string OperationType = "");
        List<T> GetAllByInteger(string Sp_Name, int Id = 0, string EnitityType = "Id", string OperationType = "");
        T CRUD(string Sp_Name,DynamicParameters parameters,int OptionalId=0);
        List<T> GETLIST(string Sp_Name,DynamicParameters parameters,int OptionalId=0);


    }

}
