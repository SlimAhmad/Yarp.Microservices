using Microsoft.AspNetCore.OData;
using Scalar.AspNetCore;
using Yarp.Microservices.Orchestrators.Brokers.Apis;
using Yarp.Microservices.Orchestrators.Brokers.DateTimes;
using Yarp.Microservices.Orchestrators.Brokers.Loggings;
using Yarp.Microservices.Orchestrators.Services.Foundations.Customers;
using Yarp.Microservices.Orchestrators.Services.Foundations.Orders;
using Yarp.Microservices.Orchestrators.Services.Processings.Orders;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddTransient<IApiBroker, ApiBroker>();
builder.Services.AddTransient<IDateTimeBroker, DateTimeBroker>();
builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();

builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IOrderProcessingService, OrderProcessingService>();

builder.Services.AddControllers().AddOData(options =>
{
    options.Select().Filter().Count().Expand().OrderBy();

 });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Allocator.Portal.Core.Api";
        options.Theme = ScalarTheme.BluePlanet;
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
        options.ShowSidebar = true;
    });
    app.UseDeveloperExceptionPage();

}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
