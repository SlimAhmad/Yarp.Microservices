// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Yarp.Microservices.Orchestrators.Models.Foundations.Orders;

namespace Yarp.Microservices.Orchestrators.Services.Foundations.Orders
{
    public interface IOrderService
    {
        ValueTask<Order> AddOrderAsync(Order order);
        ValueTask<List<Order>> RetrieveAllOrdersAsync(string oDataQuery);
        ValueTask<Order> RetrieveOrderByIdAsync(Guid orderId);
        ValueTask<Order> ModifyOrderAsync(Order order);
        ValueTask<Order> RemoveOrderByIdAsync(Guid orderId);
    }
}
