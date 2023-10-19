using MyToDo.Library.Entity;
using MyToDo.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Services
{
    public class UserService : IUserService
    {
        private readonly HttpUserRestClient userRestClient;

        public UserService(HttpUserRestClient userRestClient) 
        {
            this.userRestClient = userRestClient;
        }
        public async Task<ApiResponse> LoginAsync(string username, string password)
        {
            BaseRequest baseRequest = new BaseRequest() 
            {
                ContentType = "application/json",
                Method = RestSharp.Method.Post,
                Route = $"api/User/Login?userName={username}&passWord={password}",
            };
            var result = await userRestClient.ExecuteAsync(baseRequest);
            return result;
        }

        public async Task<ApiResponse> LogoutAsync(int id)
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                ContentType = "application/json",
                Method = RestSharp.Method.Post,
                Route = $"api/User/Register",
                Parameter = id
            };
            var result = await userRestClient.ExecuteAsync(baseRequest);
            return result;
        }

        public async Task<ApiResponse> RegisterAsync(User entity)
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                ContentType = "application/json",
                Method = RestSharp.Method.Post,
                Route = $"api/User/Register",
                Parameter = entity
            };
            var result = await userRestClient.ExecuteAsync(baseRequest);
            return result;
        }
    }
}
