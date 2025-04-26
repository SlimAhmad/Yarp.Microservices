// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace Yarp.Microservices.Orders.Api.Models.Foundations.Orders.Exceptions
{
    public class AlreadyExistsOrderException : Xeption
    {
        public AlreadyExistsOrderException(string message, Exception innerException, IDictionary data)
            : base(message, innerException, data)
        { }
    }
}