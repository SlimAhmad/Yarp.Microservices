// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Yarp.Microservices.Orders.Api.Models.Foundations.Orders.Exceptions
{
    public class OrderDependencyException : Xeption
    {
        public OrderDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}