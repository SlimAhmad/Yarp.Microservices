// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Orders.Exceptions
{
    public class InvalidOrderException : Xeption
    {
        public InvalidOrderException()
            : base(message: "Invalid Order. correct the errors and try again.")
        { }

        public InvalidOrderException(Exception innerException, IDictionary data)
            : base(message: "Invalid Order error occurred. please correct the errors and try again.",
                  innerException,
                  data)
        { }
    }
}
