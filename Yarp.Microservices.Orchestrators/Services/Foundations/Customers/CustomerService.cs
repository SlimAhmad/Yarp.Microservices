// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Yarp.Microservices.Orchestrators.Brokers.Apis;
using Yarp.Microservices.Orchestrators.Brokers.Loggings;
using Yarp.Microservices.Orchestrators.Models.Foundations.Customers;

namespace Yarp.Microservices.Orchestrators.Services.Foundations.Customers
{
    public partial class CustomerService : ICustomerService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public CustomerService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Customer> AddCustomerAsync(Customer Customer) =>
        TryCatch(async () =>
        {
            ValidateCustomerOnAdd(Customer);

            return await this.apiBroker.PostCustomerAsync(Customer);
        });

        public ValueTask<List<Customer>> RetrieveAllCustomersAsync() =>
        TryCatch(async () => await this.apiBroker.GetAllCustomersAsync());

        public ValueTask<Customer> RetrieveCustomerByIdAsync(Guid CustomerId) =>
        TryCatch(async () =>
        {
            ValidateCustomerId(CustomerId);

            return await this.apiBroker.GetCustomerByIdAsync(CustomerId);
        });

        public ValueTask<Customer> ModifyCustomerAsync(Customer Customer) =>
        TryCatch(async () =>
        {
            ValidateCustomerOnUpdate(Customer);

            return await this.apiBroker.PutCustomerAsync(Customer);
        });

        public ValueTask<Customer> RemoveCustomerByIdAsync(Guid CustomerId) =>
        TryCatch(async () =>
        {
            ValidateCustomerId(CustomerId);

            return await this.apiBroker.DeleteCustomerByIdAsync(CustomerId);
        });
    }
}
