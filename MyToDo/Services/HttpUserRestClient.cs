using MyToDo.Library.Entity;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyToDo.Services
{
    public class HttpUserRestClient
    {
        private readonly RestClient restClient;
        public HttpUserRestClient(string url)
        {
            restClient = new RestClient(url);
        }

        public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
        {
            RestRequest restRequest = new RestRequest();
            restRequest.Method = baseRequest.Method;
            restRequest.AddHeader("Content-Type", "application/json");
            if (baseRequest.Parameter != null)
            {
                restRequest.AddJsonBody(JsonConvert.SerializeObject(baseRequest.Parameter), contentType:ContentType.Json);
            }
            restRequest.Resource = Path.Combine(restClient.Options.BaseUrl.ToString(), baseRequest.Route);
            var result = await restClient.ExecuteAsync(restRequest);
            if (result.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<ApiResponse>(result.Content);
            }
            return new ApiResponse(message: "出错了");
        }
    }
}
