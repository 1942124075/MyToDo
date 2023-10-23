using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using MyToDo.Api.Exceptions;
using MyToDo.Api.Services;
using MyToDo.Api.Services.Interfaces;
using MyToDo.Api.Utility.Policys;
using MyToDo.Api.Utility.Swagger;
using MyToDo.Library.Contexts;
using MyToDo.Library.Contexts.Repositorys;
using MyToDo.Library.Entity;
using MyToDo.Library.Filters;
using MyToDo.Library.Modes;
using NLog;
using NLog.Web;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//调用NLog配置创建NLog
var logger = LogManager.Setup().LoadConfigurationFromFile("Config/NLog.Config").GetCurrentClassLogger();

#region NLog配置
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
builder.Host.UseNLog();
#endregion

// Add services to the container.

builder.Services.AddControllers(option =>
{
    //注册全局异常处理
    option.Filters.Add<CustomAsyncExceptionFilterAttribute>();
    //注册全局日志
    option.Filters.Add<CustomAsyncActionFilterAttribute>();
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//使用扩展方法配置swagger
builder.AddSwaggerFilter();
builder.AddSwaggerComments();
builder.AddSwaggerVersion();
builder.AddSwaggerToken();

//配置服务
builder.Services.AddScoped<MyDbContext>()
    .AddScoped<IMemoService, MemoService>()
    .AddScoped<IToDoService, ToDoService>()
    .AddScoped<IAuthorizationHandler, CustomAuthorizationHandler>()//策略验证
    ;

//配置仓储
builder.Services.AddCustomRepository<ToDo,ToDoRepository>()
    .AddCustomRepository<Memo, MemoRepository>()
    .AddScoped<IUnitOfWork ,UnitOfWork<MyDbContext>>();

//配置自动映射
var mapperConfig = new MapperConfiguration(config =>
{
    config.CreateMap<ToDo, ToDoDto>().ReverseMap();
    config.CreateMap<Memo, MemoDto>().ReverseMap();
    config.CreateMap<BlockItem, BlockItemDto>().ReverseMap();
    config.CreateMap<MenuItem, MenuItemDto>().ReverseMap();
    config.CreateMap<User, UserDto>().ReverseMap();
});
builder.Services.AddSingleton(mapperConfig.CreateMapper());


//读取配置文件
ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
configurationBuilder.AddJsonFile("appsettings.json",optional:true,reloadOnChange:true);
IConfigurationRoot configurationRoot = configurationBuilder.Build();
builder.Services.AddOptions().Configure<Config>(e => configurationRoot.GetSection("ConnectionStrings").Bind(e));

JWTTokenOptions tokenOptions = new JWTTokenOptions();
builder.Configuration.Bind("Token", tokenOptions);


//添加授权
builder.Services.AddAddAuthorizationEx();
//添加鉴权
builder.Services.AddAuthenticationEx(tokenOptions);

var app = builder.Build();


app.UseSwagger();
//使用扩展方法配置swagger
app.AddSwaggerVersion();
app.UseSwaggerUI();

app.UseAuthentication();//启用鉴权
app.UseAuthorization();//启用授权

app.MapControllers();


app.Run();
