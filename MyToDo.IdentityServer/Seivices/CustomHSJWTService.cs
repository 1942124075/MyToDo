using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyToDo.IdentityServer.Seivices.Interfaces;
using MyToDo.Library.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyToDo.IdentityServer.Seivices
{
    /// <summary>
    /// 对称加密的Token
    /// </summary>
    public class CustomHSJWTService : ICustomJTMService
    {
        /// <summary>
        /// JWT配置类
        /// </summary>
        private readonly IOptionsSnapshot<JWTTokenOptions> jwtOptions;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwtOptions"></param>
        public CustomHSJWTService(IOptionsSnapshot<JWTTokenOptions> jwtOptions)
        {
            this.jwtOptions = jwtOptions;
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetToken(User user)
        {
            //准备有效载荷
            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim("LastLoginDate",user.LastLoginDate.ToString()),
                new Claim("Age",user.Age.ToString()),
                new Claim("Sex",user.Sex.ToString())
            };
            //设置key
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecurityKey));
            //设置加密方式
            SigningCredentials signing = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //设置token内容
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer:jwtOptions.Value.Issuer,
                audience:jwtOptions.Value.Audience,
                claims:claims,
                expires:DateTime.Now.AddMinutes(5),
                signingCredentials:signing
                );
            //生成token
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
