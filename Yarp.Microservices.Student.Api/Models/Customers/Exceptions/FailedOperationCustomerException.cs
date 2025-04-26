// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Yarp.Microservices.Customers.Api.Tests.Unit.Services.Foundations.Customers
{
    public class FailedOperationCustomerException : Xeption
    {
        public FailedOperationCustomerException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}