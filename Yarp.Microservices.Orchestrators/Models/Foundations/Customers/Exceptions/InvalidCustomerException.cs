// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Customers.Exceptions
{
    public class InvalidCustomerException : Xeption
    {
        public InvalidCustomerException()
            : base(message: "Invalid Customer. correct the errors and try again.")
        { }

        public InvalidCustomerException(Exception innerException, IDictionary data)
            : base(message: "Invalid Customer error occurred. please correct the errors and try again.",
                  innerException,
                  data)
        { }
    }
}
