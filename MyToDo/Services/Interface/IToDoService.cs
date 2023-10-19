using MyToDo.Library.Entity;
using MyToDo.Library.Modes;
using System.Threading.Tasks;

namespace MyToDo.Services.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IToDoService : IBaseService<ToDoDto>
    {
        Task<ApiResponse<SummaryDto>> GetSummarySaync();
    }
}
