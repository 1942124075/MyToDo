using MyToDo.Library.Entity;

namespace MyToDo.Api.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMode"></typeparam>
    public interface IServiceBase<TMode>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        Task<ApiResponse<TMode>> AddAsync(TMode mode);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        Task<ApiResponse<TMode>> UpdateAsync(TMode mode);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResponse> DeleteAsync(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ApiResponse<PageList<TMode>>> GetAllAsync(QueryParameter query);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResponse<TMode>> GetSingleAsync(int id);

    }
}
