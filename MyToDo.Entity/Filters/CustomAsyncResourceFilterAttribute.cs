using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyToDo.Library.Filters
{
    /// <summary>
    /// 异步资源过滤器
    /// </summary>
    public class CustomAsyncResourceFilterAttribute : Attribute, IAsyncResourceFilter
    {
        /// <summary>
        /// 缓存字典
        /// </summary>
        private readonly Dictionary<string, object> CacheDictionary = new Dictionary<string, object>();
        /// <summary>
        /// 异步执行资源
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            string key = context.HttpContext.Request.Path;
            if (CacheDictionary.ContainsKey(key))
            {
                context.Result = CacheDictionary[key] as IActionResult;
            }
            else
            {
                var result = await next.Invoke();
                CacheDictionary[key] = result.Result;
            }
        }
    }
}
