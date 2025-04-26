// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Yarp.Microservices.Orders.Api.Models.Foundations.Orders.Exceptions
{
    public class NotFoundOrderException : Xeption
    {
        public NotFoundOrderException(string message)
            : base(message)
        { }
    }
}