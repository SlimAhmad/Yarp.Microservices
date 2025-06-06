﻿// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Yarp.Microservices.Orchestrators.Orchestrators.Models;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Customers
{
    public class Customer : IKey, IAudit
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}