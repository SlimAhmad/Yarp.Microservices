// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.AspNetCore.OData;
using Scalar.AspNetCore;
using Yarp.Microservices.Customers.Api.Brokers.DateTimes;
using Yarp.Microservices.Customers.Api.Brokers.Loggings;
using Yarp.Microservices.Customers.Api.Brokers.Storages;
using Yarp.Microservices.Customers.Api.Services.Foundations.Customers;


namespace Yarp.Microservices.Customers.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder =
                WebApplication.CreateBuilder(args);

            builder.AddSqlServerDbContext<StorageBroker>("customerDb");
            builder.AddServiceDefaults();
            builder.Services.AddControllers().AddOData(options =>
            {
                options.Select().Filter().Count().Expand().OrderBy();
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApi();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                 policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
                );
            });
            AddBrokers(builder.Services);
            AddFoundationServices(builder.Services);

            WebApplication webApplication =
                builder.Build();

            if (webApplication.Environment.IsDevelopment())
            {
                await webApplication.ConfigureDatabaseAsync();
                webApplication.MapOpenApi();
                webApplication.MapScalarApiReference(options =>
                {
                    options.Title = "Yarp.Microservices.Customers.Api";
                    options.Theme = ScalarTheme.BluePlanet;
                    options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
                    options.ShowSidebar = true;
                });
                webApplication.UseDeveloperExceptionPage();
            }

            webApplication.UseHttpsRedirection();
            webApplication.MapDefaultEndpoints();
            webApplication.UseAuthorization();
            webApplication.MapControllers();
            webApplication.Run();
        }

        private static void AddBrokers(IServiceCollection services)
        {
            services.AddTransient<IStorageBroker, StorageBroker>();
            services.AddTransient<IDateTimeBroker, DateTimeBroker>();
            services.AddTransient<ILoggingBroker, LoggingBroker>();
        }

        private static void AddFoundationServices(IServiceCollection services)
        {
            services.AddTransient<ICustomerService, CustomerService>();
        }
    }
}
