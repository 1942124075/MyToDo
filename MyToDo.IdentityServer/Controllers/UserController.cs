using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyToDo.IdentityServer.Seivices.Interfaces;
using MyToDo.Library.Entity;
using MyToDo.Library.Filters;

namespace MyToDo.IdentityServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IUserSerivce userSerivce;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UserController(ILogger<UserController> logger, IUserSerivce userSerivce)
        {
            this.logger = logger;
            this.userSerivce = userSerivce;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> LoginAsync(string userName, string passWord)
        {
            return await userSerivce.LoginAsync(userName, passWord);
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> RegisterAsync([FromBody]User user)
        {
            return await userSerivce.RegisterAsync(user);
        }
    }
}
