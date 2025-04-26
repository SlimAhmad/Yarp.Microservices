// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RESTFulSense.Controllers;
using Yarp.Microservices.Orchestrators.Models.Foundations.Orders;
using Yarp.Microservices.Orchestrators.Models.Foundations.Orders.Exceptions;
using Yarp.Microservices.Orchestrators.Services.Foundations.Orders;
using Yarp.Microservices.Orchestrators.Services.Processings.Orders;

namespace Yarp.Microservices.Orchestrators.Api.Controllers
{
    [ApiController]
    [Route("api/orchestrators/[controller]")]
    public class OrdersController : RESTFulController
    {
        private readonly IOrderProcessingService orderProcessingService;

        public OrdersController(IOrderProcessingService orderProcessingService) =>
            this.orderProcessingService = orderProcessingService;

        [HttpGet]
        [EnableQuery]
        public async ValueTask<ActionResult<IQueryable<Order>>> GetOrdersWithCustomerDetailsAsync()
        {
            try
            {
                var orders =
                    await this.orderProcessingService.RetrieveOrdersWithCustomerDetails();

                return Ok(orders);
            }
            catch (OrderDependencyException orderDependencyException)
            {
                return InternalServerError(orderDependencyException);
            }
            catch (OrderServiceException orderProcessingServiceException)
            {
                return InternalServerError(orderProcessingServiceException);
            }
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public async ValueTask<ActionResult<IQueryable<Order>>> GetAllOrdersAsync(Guid id)
        {
            try
            {
                var orders =
                    await this.orderProcessingService.RetrieveOrdersWithCustomerDetailsByIdAsync(id);

                return Ok(orders);
            }
            catch (OrderDependencyException orderDependencyException)
            {
                return InternalServerError(orderDependencyException);
            }
            catch (OrderServiceException orderProcessingServiceException)
            {
                return InternalServerError(orderProcessingServiceException);
            }
        }
    }
}
