// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Customers.Exceptions
{
    public class NullCustomerException : Xeption
    {
        public NullCustomerException() : base(message: "The Customer is null.") { }
    }
}
