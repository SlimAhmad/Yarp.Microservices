// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Processings.Orders
{
    public class NullOrderProcessingException : Xeption
    {
        public NullOrderProcessingException()
            : base(message: "Order is null.")
        { }
        public NullOrderProcessingException(string message)
            : base(message)
        { }
    }
}
