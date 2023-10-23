using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Services.Interfaces;
using MyToDo.Api.Utility.JWT;
using MyToDo.Api.Utility.Swagger;
using MyToDo.Library.Entity;
using MyToDo.Library.Modes;
using System.Security.Claims;

namespace MyToDo.Api.Controllers
{
    /// <summary>
    /// ToDo控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ApiVersion.V1))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService iService;
        private readonly IMapper mapper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iService"></param>
        /// <param name="mapper"></param>
        public ToDoController(IToDoService iService,IMapper mapper)
        {
            this.iService = iService;
            this.mapper = mapper;
        }
        /// <summary>
        /// 获取数据汇总
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<SummaryDto>> GetSummarySaync()
        {
            User user = JWTTool.ClaimsToUser(this.User.Claims);
            return await iService.GetSummarySaync(user);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse<ToDoDto>> Add([FromBody] ToDoDto mode)
        {
            User user = JWTTool.ClaimsToUser(this.User.Claims);
            ToDo toDo = mapper.Map<ToDo>(mode);
            toDo.User = user;
            return await iService.AddAsync(toDo);
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
        public async Task<ApiResponse<PageList<ToDoDto>>> GetAll([FromQuery]QueryParameter query)
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
        public async Task<ApiResponse<ToDoDto>> GetSingle(int id)
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
        public async Task<ApiResponse<ToDoDto>> Update(ToDoDto mode)
        {
            User user = JWTTool.ClaimsToUser(this.User.Claims);
            ToDo toDo = mapper.Map<ToDo>(mode);
            toDo.User = user;
            return await iService.UpdateAsync(toDo);
        }
    }
}
