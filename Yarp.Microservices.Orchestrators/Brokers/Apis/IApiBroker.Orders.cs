// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Yarp.Microservices.Orchestrators.Models.Foundations.Orders;

namespace Yarp.Microservices.Orchestrators.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<Order> PostOrderAsync(Order order);
        ValueTask<List<Order>> GetAllOrdersAsync(string oDataQuery);
        ValueTask<Order> GetOrderByIdAsync(Guid orderId);
        ValueTask<Order> PutOrderAsync(Order order);
        ValueTask<Order> DeleteOrderByIdAsync(Guid orderId);
    }
}
