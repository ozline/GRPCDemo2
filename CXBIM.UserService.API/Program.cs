using Microsoft.EntityFrameworkCore;
using CXBIM.UserService.Service.Models;
using CXBIM.UserService.API.GrpcService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<UserContext>(opt =>
    opt.UseInMemoryDatabase("Users"));
builder.Services.AddHealthChecks();
//builder.Services.Configure<ConsulServiceOptions> //健康监测内容？

builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true; //打开错误详情？
});

//builder.Services.AddGrpcClient<>
//builder.Services.AddGrpcClient<>
AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);



//builder.Services.



//加载配置文件
builder.Configuration.AddJsonFile("consulsetting.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("ocelotsetting.json", optional: false, reloadOnChange: true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) //if dev
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGrpcService<GreeterService>();

app.MapControllers();

app.Run();

