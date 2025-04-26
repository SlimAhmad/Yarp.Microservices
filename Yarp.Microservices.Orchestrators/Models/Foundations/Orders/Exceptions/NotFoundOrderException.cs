// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Orders.Exceptions
{
    public class NotFoundOrderException : Xeption
    {
        public NotFoundOrderException(Exception innerException)
            : base(message: "Not found Order error occurred, please try again.",
                  innerException)
        { }
    }
}
