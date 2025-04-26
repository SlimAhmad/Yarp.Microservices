// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Orders.Exceptions
{
    public class OrderDependencyException : Xeption
    {
        public OrderDependencyException(Xeption innerException)
            : base(message: "Order dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
