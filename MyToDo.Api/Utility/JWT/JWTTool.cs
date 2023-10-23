using MyToDo.Library.Entity;
using MyToDo.Library.Modes;
using System.Security.Claims;

namespace MyToDo.Api.Utility.JWT
{
    /// <summary>
    /// jwt工具类
    /// </summary>
    public class JWTTool
    {
        /// <summary>
        /// Claims转User
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static User ClaimsToUser(IEnumerable<Claim> claims)
        {
            
            var userId = claims.First(e => e.Type == ClaimTypes.Sid).Value;
            var userName = claims.First(e => e.Type == ClaimTypes.Name).Value;
            var userRole = claims.First(e => e.Type == ClaimTypes.Role).Value;
            var userAge = claims.First(e => e.Type == "Age").Value;
            var userSex = claims.First(e => e.Type == "Sex").Value;
            User userDto = new User() 
            {
                Id = int.Parse(userId),
                UserName = userName,
                Role = userRole,
                Age = int.Parse(userAge),
                Sex = userSex
            };
            return userDto;

        }
    }
}
