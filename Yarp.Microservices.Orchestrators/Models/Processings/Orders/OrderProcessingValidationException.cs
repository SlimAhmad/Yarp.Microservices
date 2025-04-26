// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Processings.Orders
{
    public class OrderProcessingValidationException : Xeption
    {
        public OrderProcessingValidationException(Xeption innerException)
            : base(message: "Account validation error occurred, please try again.", innerException)
        { }
        public OrderProcessingValidationException(string message,Xeption innerException)
            : base(message, innerException)
        { }
    }
}
