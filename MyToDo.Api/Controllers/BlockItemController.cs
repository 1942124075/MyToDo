using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Services.Interfaces;
using MyToDo.Api.Utility.Swagger;
using MyToDo.Library.Entity;
using MyToDo.Library.Filters;
using MyToDo.Library.Modes;

namespace MyToDo.Api.Controllers
{
    /// <summary>
    /// 块控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = false,GroupName = nameof(ApiVersion.V1))]
    public class BlockItemController : ControllerBase
    {
        private readonly IBlockItemService blockItemService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="blockItemService"></param>
        public BlockItemController(IBlockItemService blockItemService)
        {
            this.blockItemService = blockItemService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="blockItem"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse Add([FromForm] BlockItemDto blockItem)
        {
            return blockItemService.AddAsync(blockItem).Result;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResponse Delete(int id)
        {
            return blockItemService.DeleteAsync(id).Result;
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResponse GetAll([FromQuery] QueryParameter query)
        {
            return blockItemService.GetAllAsync(query).Result;
        }
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ApiResponse GetSingle(int id)
        {
            return blockItemService.GetSingleAsync(id).Result;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="blockItem"></param>
        /// <returns></returns>
        [HttpPut]
        public ApiResponse Update(BlockItemDto blockItem)
        {
            return blockItemService.UpdateAsync(blockItem).Result;
        }
    }
}
