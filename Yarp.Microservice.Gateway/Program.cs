using Yarp.Microservice.Gateway.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddReverseProxy()
    //.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
    .LoadFromMemory(ReverseProxyConfig.GetRoutes(),ReverseProxyConfig.GetClusters());

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.MapReverseProxy();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
