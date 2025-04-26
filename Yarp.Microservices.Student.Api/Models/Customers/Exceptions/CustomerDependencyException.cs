// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Yarp.Microservices.Customers.Api.Models.Foundations.Customers.Exceptions
{
    public class CustomerDependencyException : Xeption
    {
        public CustomerDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}