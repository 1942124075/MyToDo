using MyToDo.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Services.Interface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseService<TEntity> where TEntity : class
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ApiResponse<TEntity>> AddAsync(TEntity entity);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ApiResponse<TEntity>> UpdateAsync(TEntity entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResponse> DeleteAsync(int id);
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResponse<TEntity>> GetSingle(int id);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task<ApiResponse<TEntity>> GetListAsync(QueryParameter parameter);
    }
}
