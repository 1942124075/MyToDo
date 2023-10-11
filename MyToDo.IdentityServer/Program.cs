using Arch.EntityFrameworkCore.UnitOfWork;
using MyToDo.IdentityServer.Seivices;
using MyToDo.IdentityServer.Seivices.Interfaces;
using MyToDo.Library.Entity;
using MyToDo.Library.Contexts.Repositorys;
using AutoMapper;
using MyToDo.Library.Filters;
using MyToDo.Library.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(option =>
{
    option.Filters.Add<CustomAsyncExceptionFilterAttribute>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddScoped<MyDbContext>()
    .AddScoped<IUserSerivce, UserService>()
    .AddScoped<ICustomJTMService, CustomHSJWTService>()//配置对称加密token
   //.AddScoped<ICustomJTMService, CustomRSSJWTService>()//配置非对称加密token
    ;

builder.Services.AddCustomRepository<User, UserRepository>()
    .AddScoped<IUnitOfWork, UnitOfWork<MyDbContext>>();

//配置自动映射
var mapperConfig = new MapperConfiguration(config =>
{
    
});
builder.Services.AddSingleton(mapperConfig.CreateMapper());

//读取配置文件
ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
IConfigurationRoot configurationRoot = configurationBuilder.Build();
builder.Services.AddOptions().Configure<Config>(e => configurationRoot.GetSection("ConnectionStrings").Bind(e));
builder.Services.AddOptions().Configure<JWTTokenOptions>(e => configurationRoot.GetSection("Token").Bind(e));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
