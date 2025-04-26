// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Customers.Exceptions
{
    public class NotFoundCustomerException : Xeption
    {
        public NotFoundCustomerException(Exception innerException)
            : base(message: "Not found Customer error occurred, please try again.",
                  innerException)
        { }
    }
}
