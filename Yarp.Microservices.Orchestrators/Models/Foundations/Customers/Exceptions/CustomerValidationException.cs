// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Customers.Exceptions
{
    public class CustomerValidationException : Xeption
    {
        public CustomerValidationException(Exception innerException)
            : base(message: "Customer validation error occurred, please try again.",
                  innerException)
        { }
    }
}
