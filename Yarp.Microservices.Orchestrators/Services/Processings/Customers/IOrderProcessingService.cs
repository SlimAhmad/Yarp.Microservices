using Yarp.Microservices.Orchestrators.Models.Foundations.Orders;

namespace Yarp.Microservices.Orchestrators.Services.Processings.Orders
{
    public partial interface IOrderProcessingService
    {
        ValueTask<List<Order>> RetrieveOrdersWithCustomerDetails();
        ValueTask<Order> RetrieveOrdersWithCustomerDetailsByIdAsync(Guid id);
    }
}
