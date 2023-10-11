using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MyToDo.Api.Utility.Policys
{
    /// <summary>
    /// 策略的扩展,可以通过构造函数注入其他接口
    /// </summary>
    public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>
    {
        private readonly ILogger<CustomAuthorizationHandler> logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CustomAuthorizationHandler(ILogger<CustomAuthorizationHandler> logger)
        {
            this.logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizationRequirement requirement)
        {
            logger.LogInformation("策略验算");
            if (context.User.Claims.Count() == 0)
            {
                return Task.CompletedTask;//验正不通过
            }
            //做一些其他的验正...
            if (!context.User.Claims.Any(e => e.Type == ClaimTypes.Role && e.Value == "admin"))
            {
                return Task.CompletedTask;//验正不通过
            }
            context.Succeed(requirement);//通过
            return Task.CompletedTask;
        }
    }
}
