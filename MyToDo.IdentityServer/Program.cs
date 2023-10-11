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
    .AddScoped<ICustomJTMService, CustomHSJWTService>()//���öԳƼ���token
   //.AddScoped<ICustomJTMService, CustomRSSJWTService>()//���÷ǶԳƼ���token
    ;

builder.Services.AddCustomRepository<User, UserRepository>()
    .AddScoped<IUnitOfWork, UnitOfWork<MyDbContext>>();

//�����Զ�ӳ��
var mapperConfig = new MapperConfiguration(config =>
{
    
});
builder.Services.AddSingleton(mapperConfig.CreateMapper());

//��ȡ�����ļ�
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
