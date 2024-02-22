using Dapper;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace WebApi.Service
{
 
    public static class DynamicParametersHelper
    {
        public static DynamicParameters CreateParameters<T>(T obj, IEnumerable<dynamic>? arguments = null,string OperationType="")
        {
            var parameters = new DynamicParameters();


            if (arguments==null)
            {
                foreach (var propertyInfo in typeof(T).GetProperties())
                {
                    var value = propertyInfo.GetValue(obj);
                    var paramName = $"@{propertyInfo.Name}";
                    parameters.Add(paramName, value);
                }
                if (OperationType != "") parameters.Add("@OperationType",OperationType);
            }
            else
            {
                foreach (var propertyName in arguments)
                {
                    var propertyInfo = typeof(T).GetProperty(propertyName);
                    var value = propertyInfo.GetValue(obj);
                    var paramName = $"@{propertyInfo.Name}";
                    parameters.Add(paramName, value);
                }

                if(OperationType!="") parameters.Add("@OperationType",OperationType);
            }
          

            return parameters;
        }
    }

}
