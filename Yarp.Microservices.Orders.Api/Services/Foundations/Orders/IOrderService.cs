// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using GitFyle.Core.Api.Models.Foundations.Orders;

namespace Yarp.Microservices.Orders.Api.Services.Foundations.Orders
{
    public interface IOrderService
    {
        ValueTask<Order> AddOrderAsync(Order order);
        ValueTask<Order> RetrieveOrderByIdAsync(Guid orderId);
        ValueTask<IQueryable<Order>> RetrieveAllOrdersAsync();
        ValueTask<Order> ModifyOrderAsync(Order order);
        ValueTask<Order> RemoveOrderByIdAsync(Guid orderId);
    }
}