using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Yarp.Microservices.Orders.Api.Brokers.Storages;

namespace Yarp.Microservices.Orders.Api
{
    public static class WebApplicationExtensions
    {
        public static async Task ConfigureDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StorageBroker>();

            await EnsureDatabaseAsync(dbContext);
            await RunMigrationsAsync(dbContext);
        }

        private static async Task EnsureDatabaseAsync(StorageBroker dbContenxt)
        {
            var dbCreator = dbContenxt.GetService<IRelationalDatabaseCreator>();

            var strategy = dbContenxt.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                if (!await dbCreator.ExistsAsync())
                    await dbCreator.CreateAsync();
            });
        }

        private static async Task RunMigrationsAsync(StorageBroker dbContenxt)
        {
            var dbCreator = dbContenxt.GetService<IRelationalDatabaseCreator>();

            var strategy = dbContenxt.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await dbContenxt.Database.BeginTransactionAsync();
                await dbContenxt.Database.MigrateAsync();
                await transaction.CommitAsync();
            });
        }

    }
}
