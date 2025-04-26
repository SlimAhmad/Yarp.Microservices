// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Yarp.Microservices.Orchestrators.Brokers.Apis;
using Yarp.Microservices.Orchestrators.Brokers.Loggings;
using Yarp.Microservices.Orchestrators.Models.Foundations.Orders;

namespace Yarp.Microservices.Orchestrators.Services.Foundations.Orders
{
    public partial class OrderService : IOrderService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public OrderService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Order> AddOrderAsync(Order order) =>
        TryCatch(async () =>
        {
            ValidateOrderOnAdd(order);

            return await this.apiBroker.PostOrderAsync(order);
        });

        public ValueTask<List<Order>> RetrieveAllOrdersAsync(string oDataQuery = null) =>
        TryCatch(async () => await this.apiBroker.GetAllOrdersAsync(oDataQuery));

        public ValueTask<Order> RetrieveOrderByIdAsync(Guid OrderId) =>
        TryCatch(async () =>
        {
            ValidateOrderId(OrderId);

            return await this.apiBroker.GetOrderByIdAsync(OrderId);
        });

        public ValueTask<Order> ModifyOrderAsync(Order order) =>
        TryCatch(async () =>
        {
            ValidateOrderOnUpdate(order);

            return await this.apiBroker.PutOrderAsync(order);
        });

        public ValueTask<Order> RemoveOrderByIdAsync(Guid OrderId) =>
        TryCatch(async () =>
        {
            ValidateOrderId(OrderId);

            return await this.apiBroker.DeleteOrderByIdAsync(OrderId);
        });
    }
}
