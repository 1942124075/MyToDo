using MyToDo.Library.Entity;
using MyToDo.Library.Modes;

namespace MyToDo.Api.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IToDoService : IServiceBase<ToDoDto>
    {
        /// <summary>
        /// 获取汇总数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResponse<SummaryDto>> GetSummarySaync();
    }
}
