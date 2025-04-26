// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using GitFyle.Core.Api.Models.Foundations.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RESTFulSense.Controllers;
using Yarp.Microservices.Orders.Api.Models.Foundations.Orders.Exceptions;
using Yarp.Microservices.Orders.Api.Services.Foundations.Orders;

namespace Yarp.Microservices.Orders.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : RESTFulController
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService) =>
            this.orderService = orderService;

        [HttpPost]
        public async ValueTask<ActionResult<Order>> PostOrderAsync(Order order)
        {
            try
            {
                Order addedOrder =
                    await orderService.AddOrderAsync(order);

                return Created(addedOrder);
            }
            catch (OrderValidationException orderValidationException)
            {
                return BadRequest(orderValidationException.InnerException);
            }
            catch (OrderDependencyValidationException orderDependencyValidationException)
                when (orderDependencyValidationException.InnerException is AlreadyExistsOrderException)
            {
                return Conflict(orderDependencyValidationException.InnerException);
            }
            catch (OrderDependencyValidationException orderDependencyValidationException)
            {
                return BadRequest(orderDependencyValidationException.InnerException);
            }
            catch (OrderDependencyException orderDependencyException)
            {
                return InternalServerError(orderDependencyException);
            }
            catch (OrderServiceException orderServiceException)
            {
                return InternalServerError(orderServiceException);
            }
        }

        [HttpGet]
        [EnableQuery]
        public async ValueTask<ActionResult<IQueryable<Order>>> GetAllOrdersAsync()
        {
            try
            {
                IQueryable<Order> orders =
                    await this.orderService.RetrieveAllOrdersAsync();

                return Ok(orders);
            }
            catch (OrderDependencyException orderDependencyException)
            {
                return InternalServerError(orderDependencyException);
            }
            catch (OrderServiceException orderServiceException)
            {
                return InternalServerError(orderServiceException);
            }
        }

        [HttpGet("{orderId}")]
        public async ValueTask<ActionResult<Order>> GetOrderByIdAsync(Guid orderId)
        {
            try
            {
                Order order =
                    await this.orderService.RetrieveOrderByIdAsync(orderId);

                return Ok(order);
            }
            catch (OrderValidationException orderValidationException)
                when (orderValidationException.InnerException is NotFoundOrderException)
            {
                return NotFound(orderValidationException.InnerException);
            }
            catch (OrderValidationException orderValidationException)
            {
                return BadRequest(orderValidationException.InnerException);
            }
            catch (OrderDependencyValidationException orderDependencyValidationException)
            {
                return BadRequest(orderDependencyValidationException.InnerException);
            }
            catch (OrderDependencyException orderDependencyException)
            {
                return InternalServerError(orderDependencyException);
            }
            catch (OrderServiceException orderServiceException)
            {
                return InternalServerError(orderServiceException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Order>> PutOrderAsync(Order order)
        {
            try
            {
                Order modifiedOrder =
                    await this.orderService.ModifyOrderAsync(order);

                return Ok(modifiedOrder);
            }
            catch (OrderValidationException orderValidationException)
                when (orderValidationException.InnerException is NotFoundOrderException)
            {
                return NotFound(orderValidationException.InnerException);
            }
            catch (OrderValidationException orderValidationException)
            {
                return BadRequest(orderValidationException.InnerException);
            }
            catch (OrderDependencyValidationException orderDependencyValidationException)
                when (orderDependencyValidationException.InnerException is AlreadyExistsOrderException)
            {
                return Conflict(orderDependencyValidationException.InnerException);
            }
            catch (OrderDependencyValidationException orderDependencyValidationException)
            {
                return BadRequest(orderDependencyValidationException.InnerException);
            }
            catch (OrderDependencyException orderDependencyException)
            {
                return InternalServerError(orderDependencyException);
            }
            catch (OrderServiceException orderServiceException)
            {
                return InternalServerError(orderServiceException);
            }
        }

        [HttpDelete("{orderId}")]
        public async ValueTask<ActionResult<Order>> DeleteOrderByIdAsync(Guid orderId)
        {
            try
            {
                Order deletedOrder =
                    await this.orderService.RemoveOrderByIdAsync(orderId);

                return Ok(deletedOrder);
            }
            catch (OrderValidationException orderValidationException)
                when (orderValidationException.InnerException is NotFoundOrderException)
            {
                return NotFound(orderValidationException.InnerException);
            }
            catch (OrderValidationException orderValidationException)
            {
                return BadRequest(orderValidationException.InnerException);
            }
            catch (OrderDependencyValidationException orderDependencyValidationException)
                when (orderDependencyValidationException.InnerException is LockedOrderException)
            {
                return Locked(orderDependencyValidationException.InnerException);
            }
            catch (OrderDependencyValidationException orderDependencyValidationException)
            {
                return BadRequest(orderDependencyValidationException.InnerException);
            }
            catch (OrderDependencyException orderDependencyException)
            {
                return InternalServerError(orderDependencyException);
            }
            catch (OrderServiceException orderServiceException)
            {
                return InternalServerError(orderServiceException);
            }
        }
    }
}
