// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using GitFyle.Core.Api.Models.Foundations.Orders;
using Yarp.Microservices.Orders.Api.Models;

namespace Yarp.Microservices.Orders.Api.Brokers.Storages
{
    internal partial interface IStorageBroker
    {
        ValueTask<Order> InsertOrderAsync(Order order);
        ValueTask<IQueryable<Order>> SelectAllOrdersAsync();
        ValueTask<Order> SelectOrderByIdAsync(Guid orderId);
        ValueTask<Order> UpdateOrderAsync(Order order);
        ValueTask<Order> DeleteOrderAsync(Order order);
    }
}
