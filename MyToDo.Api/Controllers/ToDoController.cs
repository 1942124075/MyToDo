using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Services.Interfaces;
using MyToDo.Api.Utility.Swagger;
using MyToDo.Library.Entity;
using MyToDo.Library.Modes;

namespace MyToDo.Api.Controllers
{
    /// <summary>
    /// ToDo控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ApiVersion.V1))]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService iService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iService"></param>
        public ToDoController(IToDoService iService)
        {
            this.iService = iService;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse Add([FromForm] ToDoDto mode)
        {
            return iService.AddAsync(mode).Result;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResponse Delete(int id)
        {
            return iService.DeleteAsync(id).Result;
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResponse GetAll([FromQuery]QueryParameter query)
        {
            return iService.GetAllAsync(query).Result;
        }
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResponse GetSingle(int id)
        {
            return iService.GetSingleAsync(id).Result;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpPut]
        public ApiResponse Update(ToDoDto mode)
        {
            return iService.UpdateAsync(mode).Result;
        }
    }
}
