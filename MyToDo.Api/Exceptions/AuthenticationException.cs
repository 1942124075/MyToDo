using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using MyToDo.Api.Utility.Policys;
using MyToDo.Library.Entity;
using System.Linq;
using System.Text;

namespace MyToDo.Api.Exceptions
{
    /// <summary>
    /// 鉴\授权扩展
    /// </summary>
    public static class AuthenticationException
    {
        /// <summary>
        /// 配置鉴权
        /// </summary>
        /// <param name="services"></param>
        /// <param name="tokenOptions"></param>
        public static IServiceCollection AddAuthenticationEx(this IServiceCollection services, JWTTokenOptions tokenOptions)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//添加鉴权
            .AddJwtBearer(option =>//配置鉴权逻辑
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = tokenOptions.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
                    AudienceValidator = (m, n, z) =>
                    {
                        return true;
                    },
                    LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                    {
                        DateTime dateTime = DateTime.Now.AddHours(-8);
                        //判断token的和生效时间
                        if (dateTime < notBefore)
                        {
                            return false;
                        }
                        //判断token的有效时间
                        if (dateTime > expires)
                        {
                            return false;
                        }
                        return true;
                    }
                };
            });
            return services;
        }
        /// <summary>
        /// 配置授权
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddAddAuthorizationEx(this IServiceCollection services)
        {
            services.AddAuthorization(option =>
            {
                //添加策略
                option.AddPolicy("adminPolicy", policy =>
                {
                    policy.AddRequirements(new CustomAuthorizationRequirement());
                });
            });
            return services;
        }
    }
}
