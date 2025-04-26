// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Yarp.Microservices.Orchestrators.Models.Foundations.Customers;

namespace Yarp.Microservices.Orchestrators.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string CustomersRelativeUrl = "api/Customers";

        public async ValueTask<Customer> PostCustomerAsync(Customer customer) =>
            await this.PostAsync(CustomersRelativeUrl, customer);

        public async ValueTask<List<Customer>> GetAllCustomersAsync() =>
            await this.GetAsync<List<Customer>>(CustomersRelativeUrl);

        public async ValueTask<Customer> GetCustomerByIdAsync(Guid customerId) =>
            await this.GetAsync<Customer>($"{CustomersRelativeUrl}/{customerId}");

        public async ValueTask<Customer> PutCustomerAsync(Customer customer) =>
            await this.PutAsync(CustomersRelativeUrl, customer);

        public async ValueTask<Customer> DeleteCustomerByIdAsync(Guid customerId) =>
            await this.DeleteAsync<Customer>($"{CustomersRelativeUrl}/{customerId}");
    }
}
