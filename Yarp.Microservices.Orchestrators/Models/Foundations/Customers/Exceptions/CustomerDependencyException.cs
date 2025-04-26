// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Customers.Exceptions
{
    public class CustomerDependencyException : Xeption
    {
        public CustomerDependencyException(Xeption innerException)
            : base(message: "Customer dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
