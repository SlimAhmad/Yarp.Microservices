// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Processings.Orders
{
    public class OrderProcessingServiceException : Xeption
    {
        public OrderProcessingServiceException(Exception innerException)
            : base(message: "Failed Account processing service occurred, please contact support", innerException)
        { }
        public OrderProcessingServiceException(string message,Exception innerException)
            : base(message, innerException)
        { }
    }
}
