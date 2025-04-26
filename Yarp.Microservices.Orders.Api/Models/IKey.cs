// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;

namespace Yarp.Microservices.Orders.Api.Models
{
    public interface IKey
    {
        Guid Id { get; set; }
    }
}
