using MyToDo.Library.Entity;
using MyToDo.Library.Modes;
using MyToDo.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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

        public async Task<ApiResponse> UpdateAsync(UserDto entity)
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                ContentType = "application/json",
                Method = RestSharp.Method.Post,
                Route = $"api/User/Update",
                Parameter = entity
            };
            var result = await userRestClient.ExecuteAsync(baseRequest);
            return result;
        }
    }
}
