// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using GitFyle.Core.Api.Models.Foundations.Orders;
using Microsoft.EntityFrameworkCore;
using Yarp.Microservices.Orders.Api.Models;

namespace Yarp.Microservices.Orders.Api.Brokers.Storages
{
    internal partial class StorageBroker
    {
        public DbSet<Order> Orders { get; set; }

        public async ValueTask<Order> InsertOrderAsync(Order Order) =>
            await InsertAsync(Order);

        public async ValueTask<IQueryable<Order>> SelectAllOrdersAsync() =>
            await SelectAllAsync<Order>();

        public async ValueTask<Order> SelectOrderByIdAsync(Guid OrderId) =>
            await SelectAsync<Order>(OrderId);

        public async ValueTask<Order> UpdateOrderAsync(Order Order) =>
            await UpdateAsync(Order);

        public async ValueTask<Order> DeleteOrderAsync(Order Order) =>
            await DeleteAsync(Order);
    }
}