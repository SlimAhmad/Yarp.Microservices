﻿// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;

namespace Yarp.Microservices.Orders.Api.Models
{
    public interface IAudit
    {
        string CreatedBy { get; set; }
        DateTimeOffset CreatedDate { get; set; }
        string UpdatedBy { get; set; }
        DateTimeOffset UpdatedDate { get; set; }
    }
}
