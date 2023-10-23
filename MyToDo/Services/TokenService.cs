using Microsoft.IdentityModel.Tokens;
using MyToDo.Library.Entity;
using MyToDo.Library.Modes;
using MyToDo.Services.Interface;
using MyToDo.StaticInfo;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Services
{
    public class TokenService : ITokenService
    {
        private readonly JWTTokenOptions tokenOptions;

        public TokenService(JWTTokenOptions tokenOptions)
        {
            this.tokenOptions = tokenOptions;
        }
        /// <summary>
        /// 解密token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResponse<UserDto>> DecryptToken(string token)
        {
            try
            {
                if (tokenOptions != null && tokenOptions.SecurityKey != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(tokenOptions.SecurityKey);
                    TokenValidationParameters tokenValidationParameters = new TokenValidationParameters();
                    tokenValidationParameters.ValidIssuer = tokenOptions.Issuer;
                    tokenValidationParameters.ValidateIssuer = true;
                    tokenValidationParameters.ValidAudience = tokenOptions.Audience;
                    tokenValidationParameters.ValidateAudience = true;
                    tokenValidationParameters.ValidateIssuerSigningKey = true;
                    tokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(key);
                    TokenValidationResult result = await tokenHandler.ValidateTokenAsync(token, tokenValidationParameters);

                    var exp = long.Parse( result.Claims["exp"].ToString());
                    var nbf = long.Parse( result.Claims["nbf"].ToString());

                    DateTimeOffset expTimeOffset = DateTimeOffset.FromUnixTimeSeconds(exp);
                    DateTimeOffset nbfOffset = DateTimeOffset.FromUnixTimeSeconds(nbf);
                    DateTime expDateTime = expTimeOffset.DateTime.AddHours(8);
                    DateTime nbfDateTime = nbfOffset.DateTime.AddHours(8);
                    //if (DateTime.Now < nbfDateTime)
                    //{
                    //    return new ApiResponse<User>("令牌无效");
                    //}
                    if (DateTime.Now > expDateTime)
                    {
                        return new ApiResponse<UserDto>("令牌无效");
                    }

                    UserDto user = new UserDto();
                    StaticBase.Token = token;
                    user.UserName = result.Claims[ClaimTypes.Name].ToString();
                    user.Role = result.Claims[ClaimTypes.Role].ToString();
                    user.Age = int.Parse(result.Claims["Age"].ToString());
                    user.Id = int.Parse(result.Claims[ClaimTypes.Sid].ToString());
                    user.Sex = result.Claims["Sex"].ToString();
                    return new ApiResponse<UserDto>(true, user);
                }
                else
                {
                    return new ApiResponse<UserDto>("解密失败");
                }
            }
            catch (Exception)
            {
                return new ApiResponse<UserDto>("解密失败");
            }
        }
    }
}
