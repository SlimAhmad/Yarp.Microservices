// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Customers.Exceptions
{
    public class FailedCustomerServiceException : Xeption
    {
        public FailedCustomerServiceException(Exception innerException)
            : base(message: "Failed Customer service error occurred, contact support.",
                  innerException)
        { }
    }
}
