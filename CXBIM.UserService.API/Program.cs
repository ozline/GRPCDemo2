using Microsoft.EntityFrameworkCore;
using CXBIM.UserService.Service.Models;
using CXBIM.UserService.API.GrpcService;
using CXBIM.Core.Consul;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<UserContext>(opt =>
    opt.UseInMemoryDatabase("Users"));

//Consul
builder.Services.AddHealthChecks();
builder.Configuration.AddJsonFile("consulsetting.json", optional: false, reloadOnChange: true);
builder.Services.Configure<ConsulServiceOptions>(new
    ConfigurationBuilder()
    .AddJsonFile("consulsetting.json").Build()
    );


//ocelet
//builder.Services


//gRPC
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true; //打开错误详情？
});

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);



//builder.Services.



//加载配置文件
//builder.Configuration.AddJsonFile("ocelotsetting.json", optional: false, reloadOnChange: true);c

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) //if dev
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGrpcService<GreeterService>();

//app.UseHealthChecks()
app.UseConsul(builder.Configuration);

app.MapControllers();

app.Run();

