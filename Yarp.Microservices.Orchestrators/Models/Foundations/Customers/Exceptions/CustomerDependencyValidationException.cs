// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Customers.Exceptions
{
    public class CustomerDependencyValidationException : Xeption
    {
        public CustomerDependencyValidationException(Xeption innerException)
            : base(message: "Customer dependency validation error occurred, please try again.",
                  innerException)
        { }
    }
}
