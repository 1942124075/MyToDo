using MyToDo.Library.Entity;
using MyToDo.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly HttpRestClient client;
        private readonly string route;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="route"></param>
        public BaseService(HttpRestClient client,string route) 
        {
            this.client = client;
            this.route = route;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ApiResponse<TEntity>> AddAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/{route}/Add";
            request.Parameter = entity;
            var result =  await client.ExecuteAsync<TEntity>(request);
            return result;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse> DeleteAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Delete;
            request.Route = $"api/{route}/Delete?id={id}";
            var result = await client.ExecuteAsync(request);
            return result;
        }
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse<TEntity>> GetSingle(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/{route}/GetSingle?id={id}";
            var result = await client.ExecuteAsync<TEntity>(request);
            return result;
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<ApiResponse<PageList<TEntity>>> GetListAsync(QueryParameter parameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/{route}/GetAll?" +
                $"PageIndex={parameter.PageIndex}" +
                $"&PageSize={parameter.PageSize}" +
                $"&Search={parameter.Search}" +
                $"&Status={parameter.Status}";
            var result = await client.ExecuteAsync<PageList<TEntity>>(request);
            return result;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ApiResponse<TEntity>> UpdateAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Put;
            request.Route = $"api/{route}/Update";
            request.Parameter = entity;
            var result = await client.ExecuteAsync<TEntity>(request);
            return result;
        }
    }
}
