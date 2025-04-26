// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Customers.Exceptions
{
    public class LockedCustomerException : Xeption
    {
        public LockedCustomerException(Exception innerException)
            : base(message: "Locked Customer error occurred, please try again later.",
                  innerException)
        { }
    }
}
