using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ModelClassLibrary
{
    public class HttpUtility
    {
        public static async Task<object> CustomHttp(string BaseUrl, string Url, string content, HttpContext httpContext)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                 HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Url);

                   if (!String.IsNullOrEmpty(httpContext.Session.GetString("authorization")))
                    request.Headers.Add("authorization", httpContext.Session.GetString("authorization"));

                    request.Content = new StringContent(content, Encoding.UTF8, "application/json");


                   HttpResponseMessage Res = await client.SendAsync(request);

                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;
                    var obj = JsonConvert.DeserializeObject<Response>(response);
                    httpContext.Session.SetString("authorization", obj.Token == null ? "" : obj.Token);
                    return response;

                }
                else
                    return null;
            }
        }

        public static async Task<object> CustomHttpForGetAll(string BaseUrl, string Url, string content, HttpContext httpContext)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Url);
                request.Headers.Add("draw", httpContext.Request.Form["draw"].FirstOrDefault());
                request.Headers.Add("start", httpContext.Request.Form["start"].FirstOrDefault());
                request.Headers.Add("length", httpContext.Request.Form["length"].FirstOrDefault());
                request.Headers.Add("sortColumn", httpContext.Request.Form["columns[" + httpContext.Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault());
                request.Headers.Add("sortColumnDir", httpContext.Request.Form["order[0][dir]"].FirstOrDefault());
                request.Headers.Add("searchValue", httpContext.Request.Form["search[value]"].FirstOrDefault());



                if (!String.IsNullOrEmpty(httpContext.Session.GetString("authorization")))
                    request.Headers.Add("authorization", httpContext.Session.GetString("authorization"));
                request.Content = new StringContent(content, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.SendAsync(request);
                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;
                    var obj = JsonConvert.DeserializeObject<Response>(response);
                    httpContext.Session.SetString("authorization", obj.Token == null ? "" : obj.Token);
                    return response;

                }
                else
                    return null;
            }
        }

    }
}
