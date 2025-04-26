// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Yarp.Microservices.Customers.Api.Models.Foundations.Customers;

namespace Yarp.Microservices.Customers.Api.Services.Foundations.Customers
{
    public interface ICustomerService
    {
        ValueTask<Customer> AddCustomerAsync(Customer customer);
        ValueTask<Customer> RetrieveCustomerByIdAsync(Guid customerId);
        ValueTask<IQueryable<Customer>> RetrieveAllCustomersAsync();
        ValueTask<Customer> ModifyCustomerAsync(Customer customer);
        ValueTask<Customer> RemoveCustomerByIdAsync(Guid customerId);
    }
}