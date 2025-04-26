// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Yarp.Microservices.Customers.Api.Models;
using Yarp.Microservices.Customers.Api.Models.Foundations.Customers;

namespace Yarp.Microservices.Customers.Api.Brokers.Storages
{
    internal partial interface IStorageBroker
    {
        ValueTask<Customer> InsertCustomerAsync(Customer Customer);
        ValueTask<IQueryable<Customer>> SelectAllCustomersAsync();
        ValueTask<Customer> SelectCustomerByIdAsync(Guid CustomerId);
        ValueTask<Customer> UpdateCustomerAsync(Customer Customer);
        ValueTask<Customer> DeleteCustomerAsync(Customer Customer);
    }
}
