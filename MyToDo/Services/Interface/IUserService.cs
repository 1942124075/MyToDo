using MyToDo.Library.Entity;
using MyToDo.Library.Modes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Services.Interface
{
    public interface IUserService
    {
        Task<ApiResponse> LoginAsync(string username, string password);
        Task<ApiResponse> LogoutAsync(int id);
        Task<ApiResponse> RegisterAsync(User entity);
        Task<ApiResponse> UpdateAsync(UserDto entity);
    }
}
