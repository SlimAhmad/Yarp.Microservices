// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RESTFulSense.Controllers;
using Yarp.Microservices.Orchestrators.Models.Foundations.Customers;
using Yarp.Microservices.Orchestrators.Models.Foundations.Customers.Exceptions;
using Yarp.Microservices.Orchestrators.Services.Foundations.Customers;

namespace Yarp.Microservices.Orchestrators.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : RESTFulController
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService) =>
            this.customerService = customerService;

        [HttpPost]
        public async ValueTask<ActionResult<Customer>> PostCustomerAsync(Customer customer)
        {
            try
            {
                Customer addedCustomer =
                    await customerService.AddCustomerAsync(customer);

                return Created(addedCustomer);
            }
            catch (CustomerValidationException customerValidationException)
            {
                return BadRequest(customerValidationException.InnerException);
            }
            catch (CustomerDependencyValidationException customerDependencyValidationException)
            {
                return BadRequest(customerDependencyValidationException.InnerException);
            }
            catch (CustomerDependencyException customerDependencyException)
            {
                return InternalServerError(customerDependencyException);
            }
            catch (CustomerServiceException customerServiceException)
            {
                return InternalServerError(customerServiceException);
            }
        }

        [HttpGet]
        [EnableQuery]
        public async ValueTask<ActionResult<IQueryable<Customer>>> GetAllCustomersAsync()
        {
            try
            {
                var customers =
                    await this.customerService.RetrieveAllCustomersAsync();

                return Ok(customers);
            }
            catch (CustomerDependencyException customerDependencyException)
            {
                return InternalServerError(customerDependencyException);
            }
            catch (CustomerServiceException customerServiceException)
            {
                return InternalServerError(customerServiceException);
            }
        }

        [HttpGet("{customerId}")]
        public async ValueTask<ActionResult<Customer>> GetCustomerByIdAsync(Guid customerId)
        {
            try
            {
                Customer customer =
                    await this.customerService.RetrieveCustomerByIdAsync(customerId);

                return Ok(customer);
            }
            catch (CustomerValidationException customerValidationException)
                when (customerValidationException.InnerException is NotFoundCustomerException)
            {
                return NotFound(customerValidationException.InnerException);
            }
            catch (CustomerValidationException customerValidationException)
            {
                return BadRequest(customerValidationException.InnerException);
            }
            catch (CustomerDependencyValidationException customerDependencyValidationException)
            {
                return BadRequest(customerDependencyValidationException.InnerException);
            }
            catch (CustomerDependencyException customerDependencyException)
            {
                return InternalServerError(customerDependencyException);
            }
            catch (CustomerServiceException customerServiceException)
            {
                return InternalServerError(customerServiceException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Customer>> PutCustomerAsync(Customer customer)
        {
            try
            {
                Customer modifiedCustomer =
                    await this.customerService.ModifyCustomerAsync(customer);

                return Ok(modifiedCustomer);
            }
            catch (CustomerValidationException customerValidationException)
                when (customerValidationException.InnerException is NotFoundCustomerException)
            {
                return NotFound(customerValidationException.InnerException);
            }
            catch (CustomerValidationException customerValidationException)
            {
                return BadRequest(customerValidationException.InnerException);
            }
            catch (CustomerDependencyValidationException customerDependencyValidationException)
            {
                return BadRequest(customerDependencyValidationException.InnerException);
            }
            catch (CustomerDependencyException customerDependencyException)
            {
                return InternalServerError(customerDependencyException);
            }
            catch (CustomerServiceException customerServiceException)
            {
                return InternalServerError(customerServiceException);
            }
        }

        [HttpDelete("{customerId}")]
        public async ValueTask<ActionResult<Customer>> DeleteCustomerByIdAsync(Guid customerId)
        {
            try
            {
                Customer deletedCustomer =
                    await this.customerService.RemoveCustomerByIdAsync(customerId);

                return Ok(deletedCustomer);
            }
            catch (CustomerValidationException customerValidationException)
                when (customerValidationException.InnerException is NotFoundCustomerException)
            {
                return NotFound(customerValidationException.InnerException);
            }
            catch (CustomerValidationException customerValidationException)
            {
                return BadRequest(customerValidationException.InnerException);
            }
            catch (CustomerDependencyValidationException customerDependencyValidationException)
                when (customerDependencyValidationException.InnerException is LockedCustomerException)
            {
                return Locked(customerDependencyValidationException.InnerException);
            }
            catch (CustomerDependencyValidationException customerDependencyValidationException)
            {
                return BadRequest(customerDependencyValidationException.InnerException);
            }
            catch (CustomerDependencyException customerDependencyException)
            {
                return InternalServerError(customerDependencyException);
            }
            catch (CustomerServiceException customerServiceException)
            {
                return InternalServerError(customerServiceException);
            }
        }
    }
}
