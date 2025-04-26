// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Yarp.Microservices.Customers.Api.Models.Foundations.Customers;

namespace Yarp.Microservices.Customers.Api.Brokers.Storages
{
    internal partial class StorageBroker
    {
        public DbSet<Customer> Customers { get; set; }

        public async ValueTask<Customer> InsertCustomerAsync(Customer Customer) =>
            await InsertAsync(Customer);

        public async ValueTask<IQueryable<Customer>> SelectAllCustomersAsync() =>
            await SelectAllAsync<Customer>();

        public async ValueTask<Customer> SelectCustomerByIdAsync(Guid CustomerId) =>
            await SelectAsync<Customer>(CustomerId);

        public async ValueTask<Customer> UpdateCustomerAsync(Customer Customer) =>
            await UpdateAsync(Customer);

        public async ValueTask<Customer> DeleteCustomerAsync(Customer Customer) =>
            await DeleteAsync(Customer);
    }
}