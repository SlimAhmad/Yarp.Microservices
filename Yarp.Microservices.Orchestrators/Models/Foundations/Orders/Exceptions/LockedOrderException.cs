// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Orders.Exceptions
{
    public class LockedOrderException : Xeption
    {
        public LockedOrderException(Exception innerException)
            : base(message: "Locked Order error occurred, please try again later.",
                  innerException)
        { }
    }
}
