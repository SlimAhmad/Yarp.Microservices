// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Processings.Orders
{
    public class OrderProcessingDependencyValidationException : Xeption
    {
        public OrderProcessingDependencyValidationException(Xeption innerException)
            : base(message: "Account dependency validation error occurred, please try again.",
                innerException)
        { }

        public OrderProcessingDependencyValidationException(string message,Xeption innerException)
            : base(message,innerException)
        { }
    }
}
