using System.Linq.Expressions;
using Yarp.Microservices.Orchestrators.Brokers.Loggings;
using Yarp.Microservices.Orchestrators.Models.Foundations.Orders;
using Yarp.Microservices.Orchestrators.Services.Foundations.Orders;

namespace Yarp.Microservices.Orchestrators.Services.Processings.Orders
{
    public partial class OrderProcessingService : IOrderProcessingService
    {
      
            private readonly IOrderService  orderService;
            private readonly ILoggingBroker loggingBroker;

            public OrderProcessingService(
                IOrderService  OrderService,
                ILoggingBroker loggingBroker

                )
            {
                this.orderService = OrderService;
                this.loggingBroker = loggingBroker;
            }

            public ValueTask<List<Order>> RetrieveOrdersWithCustomerDetails() =>
            TryCatch(async () =>
            {
                string oDataQuery = BuildOdataQueryForOrderDetails();

                var orders = 
                    await this.orderService.RetrieveAllOrdersAsync(oDataQuery);

                return orders;
            });

            public ValueTask<Order> RetrieveOrdersWithCustomerDetailsByIdAsync(Guid id) =>
            TryCatch(async () =>
            {
                ValidateOrderId(id);
                string oDataQuery = BuildOdataQueryForOrderDetails(id);

                List<Order> orders = 
                    await this.orderService.RetrieveAllOrdersAsync(oDataQuery);

                Order maybeOrder = orders.FirstOrDefault();
                ValidateOrderIsNotNull(maybeOrder);

                return maybeOrder;
            });


        private static string BuildOdataQueryForOrderDetails()
        {
            return string.Concat($"?$expand={nameof(Order.Customer)}");
        }

        private static string BuildOdataQueryForOrderDetails(Guid orderId)
        {
            return string.Concat($"?$filter={nameof(Order.Id)} eq {orderId}",
                $"&$expand={nameof(Order.Customer)}");
        }
    }
}
