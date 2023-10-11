using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Services.Interfaces;
using MyToDo.Api.Utility.Swagger;
using MyToDo.Library.Entity;
using MyToDo.Library.Modes;

namespace MyToDo.Api.Controllers
{
    /// <summary>
    /// Memo控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ApiVersion.V1))]
    public class MemoController : ControllerBase
    {
        private readonly IMemoService iService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iService"></param>
        public MemoController(IMemoService iService)
        {
            this.iService = iService;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse<MemoDto>> Add([FromForm] MemoDto mode)
        {
            return await iService.AddAsync(mode);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResponse> Delete(int id)
        {
            return await iService.DeleteAsync(id);
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<PageList<MemoDto>>> GetAll([FromQuery] QueryParameter query)
        {
            return await iService.GetAllAsync(query);
        }
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<MemoDto>> GetSingle(int id)
        {
            return await iService.GetSingleAsync(id);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResponse<MemoDto>> Update(MemoDto mode)
        {
            return await iService.UpdateAsync(mode);
        }
    }
}
