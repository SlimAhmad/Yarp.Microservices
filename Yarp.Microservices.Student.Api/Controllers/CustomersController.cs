// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RESTFulSense.Controllers;
using Yarp.Microservices.Customers.Api.Models.Foundations.Customers;
using Yarp.Microservices.Customers.Api.Models.Foundations.Customers.Exceptions;
using Yarp.Microservices.Customers.Api.Services.Foundations.Customers;

namespace Yarp.Microservices.Customers.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : RESTFulController
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService) =>
            this.customerService = customerService;

        [HttpPost]
        public async ValueTask<ActionResult<Customer>> PostCustomerAsync(Customer Customer)
        {
            try
            {
                Customer addedCustomer =
                    await customerService.AddCustomerAsync(Customer);

                return Created(addedCustomer);
            }
            catch (CustomerValidationException CustomerValidationException)
            {
                return BadRequest(CustomerValidationException.InnerException);
            }
            catch (CustomerDependencyValidationException CustomerDependencyValidationException)
                when (CustomerDependencyValidationException.InnerException is AlreadyExistsCustomerException)
            {
                return Conflict(CustomerDependencyValidationException.InnerException);
            }
            catch (CustomerDependencyValidationException CustomerDependencyValidationException)
            {
                return BadRequest(CustomerDependencyValidationException.InnerException);
            }
            catch (CustomerDependencyException CustomerDependencyException)
            {
                return InternalServerError(CustomerDependencyException);
            }
            catch (CustomerserviceException CustomerserviceException)
            {
                return InternalServerError(CustomerserviceException);
            }
        }

        [HttpGet]
        [EnableQuery]
        public async ValueTask<ActionResult<IQueryable<Customer>>> GetAllCustomersAsync()
        {
            try
            {
                IQueryable<Customer> Customers =
                    await this.customerService.RetrieveAllCustomersAsync();

                return Ok(Customers);
            }
            catch (CustomerDependencyException CustomerDependencyException)
            {
                return InternalServerError(CustomerDependencyException);
            }
            catch (CustomerserviceException CustomerserviceException)
            {
                return InternalServerError(CustomerserviceException);
            }
        }

        [HttpGet("{CustomerId}")]
        public async ValueTask<ActionResult<Customer>> GetCustomerByIdAsync(Guid CustomerId)
        {
            try
            {
                Customer Customer =
                    await this.customerService.RetrieveCustomerByIdAsync(CustomerId);

                return Ok(Customer);
            }
            catch (CustomerValidationException CustomerValidationException)
                when (CustomerValidationException.InnerException is NotFoundCustomerException)
            {
                return NotFound(CustomerValidationException.InnerException);
            }
            catch (CustomerValidationException CustomerValidationException)
            {
                return BadRequest(CustomerValidationException.InnerException);
            }
            catch (CustomerDependencyValidationException CustomerDependencyValidationException)
            {
                return BadRequest(CustomerDependencyValidationException.InnerException);
            }
            catch (CustomerDependencyException CustomerDependencyException)
            {
                return InternalServerError(CustomerDependencyException);
            }
            catch (CustomerserviceException CustomerserviceException)
            {
                return InternalServerError(CustomerserviceException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Customer>> PutCustomerAsync(Customer Customer)
        {
            try
            {
                Customer modifiedCustomer =
                    await this.customerService.ModifyCustomerAsync(Customer);

                return Ok(modifiedCustomer);
            }
            catch (CustomerValidationException CustomerValidationException)
                when (CustomerValidationException.InnerException is NotFoundCustomerException)
            {
                return NotFound(CustomerValidationException.InnerException);
            }
            catch (CustomerValidationException CustomerValidationException)
            {
                return BadRequest(CustomerValidationException.InnerException);
            }
            catch (CustomerDependencyValidationException CustomerDependencyValidationException)
                when (CustomerDependencyValidationException.InnerException is AlreadyExistsCustomerException)
            {
                return Conflict(CustomerDependencyValidationException.InnerException);
            }
            catch (CustomerDependencyValidationException CustomerDependencyValidationException)
            {
                return BadRequest(CustomerDependencyValidationException.InnerException);
            }
            catch (CustomerDependencyException CustomerDependencyException)
            {
                return InternalServerError(CustomerDependencyException);
            }
            catch (CustomerserviceException CustomerserviceException)
            {
                return InternalServerError(CustomerserviceException);
            }
        }

        [HttpDelete("{CustomerId}")]
        public async ValueTask<ActionResult<Customer>> DeleteCustomerByIdAsync(Guid CustomerId)
        {
            try
            {
                Customer deletedCustomer =
                    await this.customerService.RemoveCustomerByIdAsync(CustomerId);

                return Ok(deletedCustomer);
            }
            catch (CustomerValidationException CustomerValidationException)
                when (CustomerValidationException.InnerException is NotFoundCustomerException)
            {
                return NotFound(CustomerValidationException.InnerException);
            }
            catch (CustomerValidationException CustomerValidationException)
            {
                return BadRequest(CustomerValidationException.InnerException);
            }
            catch (CustomerDependencyValidationException CustomerDependencyValidationException)
                when (CustomerDependencyValidationException.InnerException is LockedCustomerException)
            {
                return Locked(CustomerDependencyValidationException.InnerException);
            }
            catch (CustomerDependencyValidationException CustomerDependencyValidationException)
            {
                return BadRequest(CustomerDependencyValidationException.InnerException);
            }
            catch (CustomerDependencyException CustomerDependencyException)
            {
                return InternalServerError(CustomerDependencyException);
            }
            catch (CustomerserviceException CustomerserviceException)
            {
                return InternalServerError(CustomerserviceException);
            }
        }
    }
}
