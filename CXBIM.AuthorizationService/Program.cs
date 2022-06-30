using Microsoft.Extensions.Configuration;
using CXBIM.Core.Consul;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Consul
builder.Services.AddHealthChecks();  
builder.Configuration.AddJsonFile("consulsetting.json", optional: false, reloadOnChange: true);
builder.Services.Configure<ConsulServiceOptions>(new
    ConfigurationBuilder()
    .AddJsonFile("consulsetting.json").Build()
    );

//Identity4
builder.Services.AddIdentityServer(option => option.IssuerUri = ConsulChannelExtensions.GetConsuleAddress(configuration["ConsulAddress"]));
//builder.Services.AddIdentityServer().AddDeveloperSigningCredential().AddInMemoryClients()

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

//Consul
app.UseHealthChecks("/Health");
app.UseConsul(builder.Configuration);

//Identity4
app.UseIdentityServer();

app.Run();

