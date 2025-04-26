// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Processings.Orders
{
    public class InvalidOrderProcessingException : Xeption
    {
        public InvalidOrderProcessingException()
            : base(message: "Invalid Order, Please correct the errors and try again.") 
        { }
        public InvalidOrderProcessingException(string message)
            : base(message)
        { }
    }
}
