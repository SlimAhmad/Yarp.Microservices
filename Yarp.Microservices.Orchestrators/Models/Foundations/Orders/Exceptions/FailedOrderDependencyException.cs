// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Orders.Exceptions
{
    public class FailedOrderDependencyException : Xeption
    {
        public FailedOrderDependencyException(Exception innerException)
            : base(message: "Failed Order dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
