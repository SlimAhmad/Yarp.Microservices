// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using GitFyle.Core.Api.Models.Foundations.Orders;
using Yarp.Microservices.Orders.Api.Brokers.DateTimes;
using Yarp.Microservices.Orders.Api.Brokers.Loggings;
using Yarp.Microservices.Orders.Api.Brokers.Storages;
using Yarp.Microservices.Orders.Api.Models;

namespace Yarp.Microservices.Orders.Api.Services.Foundations.Orders
{
    internal partial class OrderService : IOrderService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public OrderService(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Order> AddOrderAsync(Order Order) =>
        TryCatch(async () =>
        {
            await ValidateOrderOnAddAsync(Order);

            return await this.storageBroker.InsertOrderAsync(Order);
        });

        public ValueTask<Order> RetrieveOrderByIdAsync(Guid OrderId) =>
        TryCatch(async () =>
        {
            ValidateOrderId(OrderId);

            Order maybeOrder =
                await this.storageBroker.SelectOrderByIdAsync(OrderId);

            ValidateStorageOrder(maybeOrder, OrderId);

            return maybeOrder;
        });

        public ValueTask<IQueryable<Order>> RetrieveAllOrdersAsync() =>
        TryCatch(async () => await this.storageBroker.SelectAllOrdersAsync());

        public ValueTask<Order> ModifyOrderAsync(Order Order) =>
        TryCatch(async () =>
        {
            await ValidateOrderOnModifyAsync(Order);

            Order maybeOrder =
                await this.storageBroker.SelectOrderByIdAsync(Order.Id);

            ValidateStorageOrder(maybeOrder, Order.Id);
            ValidateAgainstStorageOrderOnModify(Order, maybeOrder);

            return await this.storageBroker.UpdateOrderAsync(Order);
        });

        public ValueTask<Order> RemoveOrderByIdAsync(Guid OrderId) =>
        TryCatch(async () =>
        {
            ValidateOrderId(OrderId);

            Order maybeOrder =
                await this.storageBroker.SelectOrderByIdAsync(OrderId);

            ValidateStorageOrder(maybeOrder, OrderId);

            return await this.storageBroker.DeleteOrderAsync(maybeOrder);
        });
    }
}