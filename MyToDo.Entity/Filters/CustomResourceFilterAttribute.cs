using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyToDo.Library.Filters
{
    /// <summary>
    /// 资源过滤器
    /// </summary>
    public class CustomResourceFilterAttribute : Attribute, IResourceFilter
    {
        /// <summary>
        /// 缓存字典
        /// </summary>
        private readonly Dictionary<string, object> CacheDictionary = new Dictionary<string, object>();

        /// <summary>
        /// 资源执行之后
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            string key = context.HttpContext.Request.Path;
            CacheDictionary[key] = context.Result;
        }
        /// <summary>
        /// 资源执行之前
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string key = context.HttpContext.Request.Path;
            if (CacheDictionary.ContainsKey(key))
            {
                context.Result = CacheDictionary[key] as IActionResult;
            }
        }
    }
}
