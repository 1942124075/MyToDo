using Microsoft.OpenApi.Models;

namespace MyToDo.Api.Utility.Swagger
{
    /// <summary>
    /// swagger扩展类
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// 配置swagger版本
        /// </summary>
        /// <param name="builder"></param>
        public static void AddSwaggerVersion(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(option =>
            {
                #region 配置swagger版本
                typeof(ApiVersion).GetEnumNames().ToList().ForEach(name =>
                {
                    option.SwaggerDoc(name, new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "ToDo API",
                        Version = name,
                        Description = $"ToDo API {name}版本"
                    });
                });
                #endregion
            });
        }
        /// <summary>
        /// 配置api方法显示注释
        /// </summary>
        /// <param name="builder"></param>
        public static void AddSwaggerComments(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(option =>
            {
                #region 配置api方法显示注释
                var file = Path.Combine(AppContext.BaseDirectory, "MyToDo.Api.xml");
                option.IncludeXmlComments(file, true);
                option.OrderActionsBy(o => o.RelativePath);
                #endregion
            });
        }

        /// <summary>
        /// 配置文件上传按钮
        /// </summary>
        /// <param name="builder"></param>
        public static void AddSwaggerFilter(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(option =>
            {
                #region 配置文件上传按钮
                option.OperationFilter<FileUploadFilter>();
                #endregion
            });
        }

        /// <summary>
        /// 配置Token验证
        /// </summary>
        /// <param name="builder"></param>
        public static void AddSwaggerToken(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(option =>
            {
                //添加安全定义
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "请输入token，格式为Bearer xxxxx(注意中间有空格)",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                //添加安全要求
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { 
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                    }
                });
            });
        }

        /// <summary>
        /// 配置swagger版本
        /// </summary>
        /// <param name="application"></param>
        public static void AddSwaggerVersion(this WebApplication application)
        {
            application.UseSwaggerUI(option =>
            {
                typeof(ApiVersion).GetEnumNames().ToList().ForEach(name =>
                {
                    option.SwaggerEndpoint($"/swagger/{name}/swagger.json", name);
                });
            });
        }

    }
}
