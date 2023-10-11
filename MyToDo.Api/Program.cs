using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyToDo.Api.Services;
using MyToDo.Api.Services.Interfaces;
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

//����NLog���ô���NLog
var logger = LogManager.Setup().LoadConfigurationFromFile("Config/NLog.Config").GetCurrentClassLogger();

#region NLog����
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
builder.Host.UseNLog();
#endregion

// Add services to the container.

builder.Services.AddControllers(option =>
{
    //ע��ȫ���쳣����
    option.Filters.Add<CustomExceptionAttribute>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//ʹ����չ��������swagger
builder.AddSwaggerFilter();
builder.AddSwaggerComments();
builder.AddSwaggerVersion();
builder.AddSwaggerToken();

//���÷���
builder.Services.AddScoped<MyDbContext>()
    .AddScoped<IBlockItemService, BlockItemService>()
    .AddScoped<IMemoService, MemoService>()
    .AddScoped<IMenuItemService, MenuItemService>()
    .AddScoped<IToDoService, ToDoService>();

//���òִ�
builder.Services.AddCustomRepository<ToDo,ToDoRepository>()
    .AddCustomRepository<Memo, MemoRepository>()
    .AddCustomRepository<BlockItem, BlockItemRepository>()
    .AddCustomRepository<MenuItem, MenuItemRepository>()
    .AddScoped<IUnitOfWork ,UnitOfWork<MyDbContext>>();

//�����Զ�ӳ��
var mapperConfig = new MapperConfiguration(config =>
{
    config.CreateMap<ToDo, ToDoDto>().ReverseMap();
    config.CreateMap<Memo, MemoDto>().ReverseMap();
    config.CreateMap<BlockItem, BlockItemDto>().ReverseMap();
    config.CreateMap<MenuItem, MenuItemDto>().ReverseMap();
});
builder.Services.AddSingleton(mapperConfig.CreateMapper());


//��ȡ�����ļ�
ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
configurationBuilder.AddJsonFile("appsettings.json",optional:true,reloadOnChange:true);
IConfigurationRoot configurationRoot = configurationBuilder.Build();
builder.Services.AddOptions().Configure<Config>(e => configurationRoot.GetSection("ConnectionStrings").Bind(e));

JWTTokenOptions tokenOptions = new JWTTokenOptions();
builder.Configuration.Bind("Token", tokenOptions);


builder.Services.AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>//���ü�Ȩ�߼�
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
            AudienceValidator = (m,n,z) =>
            {
                return true;
            },
            LifetimeValidator = (notBefore,expires,securityToken,validationParameters) =>
            {
                return true;
            }
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //ʹ����չ��������swagger
    app.AddSwaggerVersion();
}

app.UseAuthentication();//���ü�Ȩ
app.UseAuthorization();//������Ȩ

app.MapControllers();


app.Run();
