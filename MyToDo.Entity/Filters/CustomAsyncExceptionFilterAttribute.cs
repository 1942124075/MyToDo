using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MyToDo.Library.Entity;

namespace MyToDo.Library.Filters
{
    /// <summary>
    /// 异步异常过滤器
    /// </summary>
    public class CustomAsyncExceptionFilterAttribute : Attribute, IAsyncExceptionFilter
    {
        private readonly ILogger<CustomAsyncExceptionFilterAttribute> logger;

        public CustomAsyncExceptionFilterAttribute(ILogger<CustomAsyncExceptionFilterAttribute> logger)
        {
            this.logger = logger;
        }
        /// <summary>
        /// 执行异步异常过滤器
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                logger.LogError(exception: context.Exception,message: context.Exception.Message);
                context.ExceptionHandled = true;
                ApiResponse apiResponse = new ApiResponse(context.Exception.Message);
                context.Result = new JsonResult(apiResponse);
            }
            return Task.CompletedTask;
        }
    }
}
