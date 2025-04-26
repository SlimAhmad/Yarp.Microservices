// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Customers.Exceptions
{
    public class FailedCustomerDependencyException : Xeption
    {
        public FailedCustomerDependencyException(Exception innerException)
            : base(message: "Failed Customer dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
