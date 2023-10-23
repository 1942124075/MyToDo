using MyToDo.Library.Entity;
using MyToDo.Library.Modes;
using MyToDo.StaticInfo;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
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
        /// 发送请求
        /// </summary>
        /// <param name="baseRequest"></param>
        /// <returns></returns>
        public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
        {
            var request = new RestRequest();
            request.Method = baseRequest.Method;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {StaticBase.Token}");
            if (baseRequest.Parameter != null)
            {
                request.AddBody(JsonConvert.SerializeObject(baseRequest.Parameter), contentType: ContentType.Json);
            }
            request.Resource = baseRequest.Route;
            RestResponse response = await _client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<ApiResponse>(response.Content);
        }
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseRequest"></param>
        /// <returns></returns>
        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        {
            var request = new RestRequest();
            request.Method = baseRequest.Method;
            request.AddHeader("Content-Type", baseRequest.ContentType);
            request.AddHeader("Authorization", $"Bearer {StaticBase.Token}" );
            //request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMTIzIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiYWRtaW4iLCJMYXN0TG9naW5EYXRlIjoiMjAyMy8xMC8xMCDmmJ_mnJ_kuowgODozMTo1MCIsIkFnZSI6IjMiLCJTZXgiOiIyIiwiZXhwIjoxNjk3MDM5Nzc1LCJpc3MiOiJ6enh4Y2N2dmRkc3NhYXF3d2UiLCJhdWQiOiJxd2VydHl1aW9wYXNkZmdocSJ9.4hUu79nKyEawPXLQXjrYnBVyn9aOIGHi10r6PkNzESk");
            if (baseRequest.Parameter != null)
            {
                request.AddBody(JsonConvert.SerializeObject(baseRequest.Parameter), ContentType.Json);
            }
            request.Resource = Path.Combine(_client.Options.BaseUrl + baseRequest.Route);
            RestResponse response = await _client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                return new ApiResponse<T>("数据加载失败!");
            }
            return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
        }
    }
}
