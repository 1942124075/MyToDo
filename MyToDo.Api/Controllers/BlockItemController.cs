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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "adminPolicy")]
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
        
        public async Task<ApiResponse<BlockItemDto>> Add([FromForm] BlockItemDto blockItem)
        {
            return await blockItemService.AddAsync(blockItem);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResponse> Delete(int id)
        {
            return await blockItemService.DeleteAsync(id);
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<PageList<BlockItemDto>>> GetAll([FromQuery] QueryParameter query)
        {
            return await blockItemService.GetAllAsync(query);
        }
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<BlockItemDto>> GetSingle(int id)
        {
            return await blockItemService.GetSingleAsync(id);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="blockItem"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResponse<BlockItemDto>> Update(BlockItemDto blockItem)
        {
            return await blockItemService.UpdateAsync(blockItem);
        }
    }
}
