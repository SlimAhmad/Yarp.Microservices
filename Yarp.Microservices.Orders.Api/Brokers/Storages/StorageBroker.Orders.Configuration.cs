// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using GitFyle.Core.Api.Models.Foundations.Orders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarp.Microservices.Orders.Api.Models;

namespace Yarp.Microservices.Orders.Api.Brokers.Storages
{
    internal partial class StorageBroker
    {
        static void AddOrderConfigurations(EntityTypeBuilder<Order> builder)
        {
            builder.Property(order => order.ProductName)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
