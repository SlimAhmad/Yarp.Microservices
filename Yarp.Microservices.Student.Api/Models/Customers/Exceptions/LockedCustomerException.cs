// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace Yarp.Microservices.Customers.Api.Models.Foundations.Customers.Exceptions
{
    public class LockedCustomerException : Xeption
    {
        public LockedCustomerException(string message, Exception innerException, IDictionary data)
            : base(message, innerException, data)
        { }
    }
}