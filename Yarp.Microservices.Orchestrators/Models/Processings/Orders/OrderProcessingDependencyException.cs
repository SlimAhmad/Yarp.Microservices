// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Processings.Orders
{
    public class OrderProcessingDependencyException : Xeption
    {
        public OrderProcessingDependencyException(Xeption innerException)
            : base(message: "Order dependency error occurred, please contact support", innerException)
        { }
        public OrderProcessingDependencyException(string message,Xeption innerException)
            : base(message, innerException)
        { }
    }
}
