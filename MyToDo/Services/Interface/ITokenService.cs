using MyToDo.Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Services.Interface
{
    public interface ITokenService
    {
        /// <summary>
        /// 解密token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ApiResponse<User>> DecryptToken(string token);
    }
}
