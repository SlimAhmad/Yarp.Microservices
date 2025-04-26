// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarp.Microservices.Customers.Api.Models;
using Yarp.Microservices.Customers.Api.Models.Foundations.Customers;

namespace Yarp.Microservices.Customers.Api.Brokers.Storages
{
    internal partial class StorageBroker
    {
        static void AddCustomerConfigurations(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(Customer => Customer.Name)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
