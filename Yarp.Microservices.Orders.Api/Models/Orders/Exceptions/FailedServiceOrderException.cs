// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Yarp.Microservices.Orders.Api.Models.Foundations.Orders.Exceptions
{
    public class FailedServiceOrderException : Xeption
    {
        public FailedServiceOrderException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}