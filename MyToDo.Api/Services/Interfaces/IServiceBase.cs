using MyToDo.Library.Entity;
using MyToDo.Library.Modes;

namespace MyToDo.Api.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMode"></typeparam>
    /// <typeparam name="TReturn"></typeparam>
    public interface IServiceBase<TMode,TReturn>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        Task<ApiResponse<TReturn>> AddAsync(TMode mode);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        Task<ApiResponse<TReturn>> UpdateAsync(TMode mode);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResponse> DeleteAsync(int id);
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="query"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ApiResponse<PageList<TReturn>>> GetAllAsync(QueryParameter query,User user);
        /// <summary>
        /// 查询单个
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ApiResponse<TReturn>> GetSingleAsync(int id,User user);

    }
}
