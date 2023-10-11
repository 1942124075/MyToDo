using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyToDo.Library.Entity;

namespace MyToDo.Library.Filters
{
    /// <summary>
    /// 异常过滤器
    /// </summary>
    public class CustomExceptionAttribute : Attribute, IExceptionFilter
    {
        /// <summary>
        /// 执行异常过滤器
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                context.ExceptionHandled = true;
                ApiResponse apiResponse = new ApiResponse(context.Exception.Message);
                context.Result = new JsonResult(apiResponse);
            }
        }
    }
}
