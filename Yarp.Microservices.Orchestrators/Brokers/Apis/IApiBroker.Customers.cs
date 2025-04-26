// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Yarp.Microservices.Orchestrators.Models.Foundations.Customers;

namespace Yarp.Microservices.Orchestrators.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<Customer> PostCustomerAsync(Customer customer);
        ValueTask<List<Customer>> GetAllCustomersAsync();
        ValueTask<Customer> GetCustomerByIdAsync(Guid customerId);
        ValueTask<Customer> PutCustomerAsync(Customer customer);
        ValueTask<Customer> DeleteCustomerByIdAsync(Guid customerId);
    }
}
