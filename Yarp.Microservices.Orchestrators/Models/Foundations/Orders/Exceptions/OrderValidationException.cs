// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Orders.Exceptions
{
    public class OrderValidationException : Xeption
    {
        public OrderValidationException(Exception innerException)
            : base(message: "Order validation error occurred, please try again.",
                  innerException)
        { }
    }
}
