// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Yarp.Microservices.Orders.Api.Tests.Unit.Services.Foundations.Orders
{
    public class FailedOperationOrderException : Xeption
    {
        public FailedOperationOrderException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}