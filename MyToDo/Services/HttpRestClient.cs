using MyToDo.Entity;
using MyToDo.Library.Entity;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Services
{
    public class HttpRestClient
    {
        private readonly RestClient _client;
        public HttpRestClient(string url) 
        {
            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1,
            };
            _client = new RestClient(options);
        }

        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="baseRequest"></param>
        /// <returns></returns>
        public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
        {
            var request = new RestRequest();
            request.Method = baseRequest.Method;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMTIzIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiYWRtaW4iLCJMYXN0TG9naW5EYXRlIjoiMjAyMy8xMC8xMCDmmJ_mnJ_kuowgODozMTo1MCIsIkFnZSI6IjMiLCJTZXgiOiIyIiwiZXhwIjoxNjk3MDM5Nzc1LCJpc3MiOiJ6enh4Y2N2dmRkc3NhYXF3d2UiLCJhdWQiOiJxd2VydHl1aW9wYXNkZmdocSJ9.4hUu79nKyEawPXLQXjrYnBVyn9aOIGHi10r6PkNzESk");
            if (baseRequest.Parameter != null)
            {
                request.AddParameter("param",JsonConvert.SerializeObject(baseRequest), ParameterType.RequestBody);
            }
            request.Resource = baseRequest.Route;
            RestResponse response = await _client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<ApiResponse>(response.Content);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseRequest"></param>
        /// <returns></returns>
        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        {
            var request = new RestRequest();
            request.Method = baseRequest.Method;
            request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMTIzIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiYWRtaW4iLCJMYXN0TG9naW5EYXRlIjoiMjAyMy8xMC8xMCDmmJ_mnJ_kuowgODozMTo1MCIsIkFnZSI6IjMiLCJTZXgiOiIyIiwiZXhwIjoxNjk3MDM5Nzc1LCJpc3MiOiJ6enh4Y2N2dmRkc3NhYXF3d2UiLCJhdWQiOiJxd2VydHl1aW9wYXNkZmdocSJ9.4hUu79nKyEawPXLQXjrYnBVyn9aOIGHi10r6PkNzESk");
            if (baseRequest.Parameter != null)
            {
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest), ParameterType.RequestBody);
            }
            request.Resource = Path.Combine(_client.Options.BaseUrl + baseRequest.Route);
            RestResponse response = await _client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
        }
    }
}
