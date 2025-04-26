// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Orders.Exceptions
{
    public class OrderDependencyValidationException : Xeption
    {
        public OrderDependencyValidationException(Xeption innerException)
            : base(message: "Order dependency validation error occurred, please try again.",
                  innerException)
        { }
    }
}
