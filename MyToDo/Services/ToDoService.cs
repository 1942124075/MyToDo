using MyToDo.Library.Entity;
using MyToDo.Library.Modes;
using MyToDo.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Services
{
    public class ToDoService : BaseService<ToDoDto>, IToDoService
    {
        private readonly HttpRestClient client;

        public ToDoService(HttpRestClient client) : base(client, "ToDo")
        {
            this.client = client;
        }
        /// <summary>
        /// 获取数据汇总
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse<SummaryDto>> GetSummarySaync()
        {
            BaseRequest baseRequest = new BaseRequest() 
            {
                ContentType = "application/json",
                Method = RestSharp.Method.Get,
                Route = "api/ToDo/GetSummarySaync"
            };
            return await client.ExecuteAsync<SummaryDto>(baseRequest);
        }
    }
}
