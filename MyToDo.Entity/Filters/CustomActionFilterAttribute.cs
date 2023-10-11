using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace MyToDo.Library.Filters
{
    /// <summary>
    /// 方法过滤器
    /// </summary>
    public class CustomActionFilterAttribute : Attribute, IActionFilter
    {
        private readonly ILogger<CustomActionFilterAttribute> logger;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="logger"></param>
        public CustomActionFilterAttribute(ILogger<CustomActionFilterAttribute> logger)
        {
            this.logger = logger;
        }
        /// <summary>
        /// 方法执行之后
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            string? controller = context.RouteData.Values["controller"]?.ToString();
            string? action = context.RouteData.Values["action"]?.ToString();
            logger.LogInformation($"{controller}--{action} 执行完成");
        }
        /// <summary>
        /// 方法执行之前
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string? controller = context.RouteData.Values["controller"]?.ToString();
            string? action = context.RouteData.Values["action"]?.ToString();
            logger.LogInformation($"{controller}--{action} 执行之前");
        }
    }
}
