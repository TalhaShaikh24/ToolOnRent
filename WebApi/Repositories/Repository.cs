using Dapper;
using ModelClassLibrary;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using WebApi.DBManager;
using WebApi.IRepositories;

namespace WebApi.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDapper _dapper;


        public Repository(IDapper dapper)
        {
            _dapper = dapper;

        }

        public T CRUD(string Sp_Name, DynamicParameters parameters, int OptionalId = 0)
        {
            var result = _dapper.Insert<T>(Sp_Name, parameters);

            return result;
        }
        public List<T> GETLIST(string Sp_Name, DynamicParameters parameters, int OptionalId = 0)
        {
            var result = _dapper.GetAll<T>(Sp_Name, parameters);

            return result;
        }

        public List<T> GetAll(string Sp_Name,int Id=0, string EnitityType = "Id",string OperationType = "Read")
        {
            DynamicParameters parameters =new DynamicParameters();
            if(Id > 0) parameters.Add($"@{EnitityType}",Id, DbType.Int32);
            parameters.Add("@OperationType", OperationType,DbType.String);
            var result = _dapper.GetAll<T>(Sp_Name, parameters);

            return result;
        }

        public List<T> GetAllWithOutOperationType(string Sp_Name)
        {
            DynamicParameters parameters = new DynamicParameters();
            var result = _dapper.GetAll<T>(Sp_Name, parameters);

            return result;
        }

        public List<T> GetAllByInteger(string Sp_Name, int Id = 0, string EnitityType = "Id", string OperationType = "")
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{EnitityType}", Id, DbType.String);
            if (OperationType != "") parameters.Add("@OperationType", OperationType, DbType.String);
            var result = _dapper.GetAll<T>(Sp_Name, parameters);
            return result;
        }

        public List<T> GetAllByString(string Sp_Name, string Id = "", string EnitityType = "Id", string OperationType = "")
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{EnitityType}", Id, DbType.String);
            if(OperationType!="")parameters.Add("@OperationType", OperationType, DbType.String);
            var result = _dapper.GetAll<T>(Sp_Name, parameters);

            return result;
        }
    }

}
