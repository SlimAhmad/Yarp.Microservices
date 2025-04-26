// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Yarp.Microservices.Orchestrators.Models.Foundations.Customers;
using Yarp.Microservices.Orchestrators.Orchestrators.Models;

namespace Yarp.Microservices.Orchestrators.Models.Foundations.Orders
{
    public class Order : IKey, IAudit
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string CustomerId { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        public List<Customer> Customer { get; set; }
    }
}