using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyToDo.IdentityServer.Seivices.Interfaces;
using MyToDo.Library.Entity;
using MyToDo.Library.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MyToDo.IdentityServer.Seivices
{
    /// <summary>
    /// 非对称加密token
    /// </summary>
    public class CustomRSSJWTService : ICustomJTMService
    {
        private readonly IOptionsSnapshot<JWTTokenOptions> jwtOptions;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwtOptions"></param>
        public CustomRSSJWTService(IOptionsSnapshot<JWTTokenOptions> jwtOptions)
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
            string keyDir = Directory.GetCurrentDirectory();
            if (!RSAHelper.TryGetKeyParameters(keyDir,false,out RSAParameters parameters))
            {
                parameters = RSAHelper.GenerateAndSaveKey(keyDir,false);
            }
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
            RsaSecurityKey rsaSecurity = new RsaSecurityKey(parameters);
            //设置加密方式
            SigningCredentials signing = new SigningCredentials(rsaSecurity, SecurityAlgorithms.RsaSha256Signature);
            //设置token内容
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: jwtOptions.Value.Issuer,
                audience: jwtOptions.Value.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signing
                );
            //生成token
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
