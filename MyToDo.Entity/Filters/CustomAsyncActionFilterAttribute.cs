using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace MyToDo.Library.Filters
{
    /// <summary>
    /// 异步方法过滤器
    /// </summary>
    public class CustomAsyncActionFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly ILogger<CustomAsyncActionFilterAttribute> logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CustomAsyncActionFilterAttribute(ILogger<CustomAsyncActionFilterAttribute> logger)
        {
            this.logger = logger;
        }
        /// <summary>
        /// 执行异步方法过滤器
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string? controller = context.RouteData.Values["controller"]?.ToString();
            string? action = context.RouteData.Values["action"]?.ToString();
            logger.LogInformation($"{controller}--{action} 异步执行之前");
            await next.Invoke();
            logger.LogInformation($"{controller}--{action} 异步执行完成");
        }
    }
}
