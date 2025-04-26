// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Yarp.Microservices.Customers.Api.Brokers.DateTimes;
using Yarp.Microservices.Customers.Api.Brokers.Loggings;
using Yarp.Microservices.Customers.Api.Brokers.Storages;
using Yarp.Microservices.Customers.Api.Models;
using Yarp.Microservices.Customers.Api.Models.Foundations.Customers;

namespace Yarp.Microservices.Customers.Api.Services.Foundations.Customers
{
    internal partial class CustomerService : ICustomerService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public CustomerService(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Customer> AddCustomerAsync(Customer customer) =>
        TryCatch(async () =>
        {
            await ValidateCustomerOnAddAsync(customer);

            return await this.storageBroker.InsertCustomerAsync(customer);
        });

        public ValueTask<Customer> RetrieveCustomerByIdAsync(Guid CustomerId) =>
        TryCatch(async () =>
        {
            ValidateCustomerId(CustomerId);

            Customer maybeCustomer =
                await this.storageBroker.SelectCustomerByIdAsync(CustomerId);

            ValidateStorageCustomer(maybeCustomer, CustomerId);

            return maybeCustomer;
        });

        public ValueTask<IQueryable<Customer>> RetrieveAllCustomersAsync() =>
        TryCatch(async () => await this.storageBroker.SelectAllCustomersAsync());

        public ValueTask<Customer> ModifyCustomerAsync(Customer customer) =>
        TryCatch(async () =>
        {
            await ValidateCustomerOnModifyAsync(customer);

            Customer maybeCustomer =
                await this.storageBroker.SelectCustomerByIdAsync(customer.Id);

            ValidateStorageCustomer(maybeCustomer, customer.Id);
            ValidateAgainstStorageCustomerOnModify(customer, maybeCustomer);

            return await this.storageBroker.UpdateCustomerAsync(customer);
        });

        public ValueTask<Customer> RemoveCustomerByIdAsync(Guid CustomerId) =>
        TryCatch(async () =>
        {
            ValidateCustomerId(CustomerId);

            Customer maybeCustomer =
                await this.storageBroker.SelectCustomerByIdAsync(CustomerId);

            ValidateStorageCustomer(maybeCustomer, CustomerId);

            return await this.storageBroker.DeleteCustomerAsync(maybeCustomer);
        });
    }
}