// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Yarp.Microservices.Orchestrators.Models.Foundations.Orders;

namespace Yarp.Microservices.Orchestrators.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string OrdersRelativeUrl = "api/Orders";

        public async ValueTask<Order> PostOrderAsync(Order order) =>
            await this.PostAsync(OrdersRelativeUrl, order);

        public async ValueTask<List<Order>> GetAllOrdersAsync(string oDataQuery = null) =>
            await this.GetAsync<List<Order>>($"{OrdersRelativeUrl}{oDataQuery}");

        public async ValueTask<Order> GetOrderByIdAsync(Guid orderId) =>
            await this.GetAsync<Order>($"{OrdersRelativeUrl}/{orderId}");

        public async ValueTask<Order> PutOrderAsync(Order order) =>
            await this.PutAsync(OrdersRelativeUrl, order);

        public async ValueTask<Order> DeleteOrderByIdAsync(Guid orderId) =>
            await this.DeleteAsync<Order>($"{OrdersRelativeUrl}/{orderId}");
    }
}
