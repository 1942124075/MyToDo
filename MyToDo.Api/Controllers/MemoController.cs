using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Services.Interfaces;
using MyToDo.Api.Utility.JWT;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MemoController : ControllerBase
    {
        private readonly IMemoService iService;
        private readonly IMapper mapper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iService"></param>
        /// <param name="mapper"></param>
        public MemoController(IMemoService iService,IMapper mapper)
        {
            this.iService = iService;
            this.mapper = mapper;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse<MemoDto>> Add([FromBody] MemoDto mode)
        {
            User user = JWTTool.ClaimsToUser(this.User.Claims);
            Memo memo = mapper.Map<Memo>(mode);
            memo.User = user;
            return await iService.AddAsync(memo);
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
            User user = JWTTool.ClaimsToUser(this.User.Claims);
            return await iService.GetAllAsync(query, user);
        }
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<MemoDto>> GetSingle(int id)
        {
            User user = JWTTool.ClaimsToUser(this.User.Claims);
            return await iService.GetSingleAsync(id, user);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResponse<MemoDto>> Update(MemoDto mode)
        {
            User user = JWTTool.ClaimsToUser(this.User.Claims);
            Memo memo = mapper.Map<Memo>(mode);
            memo.User = user;
            return await iService.UpdateAsync(memo);
        }
    }
}
