using Microsoft.EntityFrameworkCore;
using CXBIM.UserService.Service.Models;
using CXBIM.UserService.API.GrpcService;
using CXBIM.Core.Consul;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//使用内存数据库
builder.Services.AddDbContext<UserContext>(opt =>
    opt.UseInMemoryDatabase("Users"));

//Consul
builder.Services.AddHealthChecks();
builder.Configuration.AddJsonFile("consulsetting.json", optional: false, reloadOnChange: true);
builder.Services.Configure<ConsulServiceOptions>(new
    ConfigurationBuilder()
    .AddJsonFile("consulsetting.json").Build()
    );

//gRPC
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true; //打开错误详情？
});

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) //if dev
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGrpcService<GreeterService>();

//Consul
app.UseHealthChecks("/Health");
app.UseConsul(builder.Configuration);

app.MapControllers();

app.Run();

