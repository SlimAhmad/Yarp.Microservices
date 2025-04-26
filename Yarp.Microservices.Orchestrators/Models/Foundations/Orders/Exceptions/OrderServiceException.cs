// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Orders.Exceptions
{
    public class OrderServiceException : Xeption
    {
        public OrderServiceException(Xeption innerException)
            : base(message: "Order service error occurred, contact support.",
                  innerException)
        { }
    }
}
