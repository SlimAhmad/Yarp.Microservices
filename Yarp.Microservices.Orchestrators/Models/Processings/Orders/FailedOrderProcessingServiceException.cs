// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Processings.Orders
{
    public class FailedOrderProcessingServiceException : Xeption
    {
        public FailedOrderProcessingServiceException(Exception innerException)
            : base(message: "Failed Order service occurred, please contact support", innerException)
        { }

        public FailedOrderProcessingServiceException(string message,Exception innerException)
            : base(message, innerException)
        { }
    }
}
