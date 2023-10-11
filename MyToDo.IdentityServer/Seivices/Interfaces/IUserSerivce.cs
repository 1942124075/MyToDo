using Microsoft.AspNetCore.Mvc;
using MyToDo.Library.Entity;

namespace MyToDo.IdentityServer.Seivices.Interfaces
{
    public interface IUserSerivce
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<ApiResponse> LoginAsync(string username, string password);
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ApiResponse> RegisterAsync(User user);
    }
}
