using MyToDo.Library.Entity;

namespace MyToDo.Api.Services.Interfaces
{
    public interface IServiceBase<TMode>
    {
        Task<ApiResponse> AddAsync(TMode mode);

        Task<ApiResponse> UpdateAsync(TMode mode);

        Task<ApiResponse> DeleteAsync(int id);

        Task<ApiResponse> GetAllAsync(QueryParameter query);

        Task<ApiResponse> GetSingleAsync(int id);

    }
}
