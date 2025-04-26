// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Customers.Exceptions
{
    public class CustomerServiceException : Xeption
    {
        public CustomerServiceException(Xeption innerException)
            : base(message: "Customer service error occurred, contact support.",
                  innerException)
        { }
    }
}
