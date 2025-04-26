// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Xeptions;
using Yarp.Microservices.Customers.Api.Models;
using Yarp.Microservices.Customers.Api.Models.Foundations.Customers;
using Yarp.Microservices.Customers.Api.Models.Foundations.Customers.Exceptions;
using Yarp.Microservices.Customers.Api.Tests.Unit.Services.Foundations.Customers;

namespace Yarp.Microservices.Customers.Api.Services.Foundations.Customers
{
    internal partial class CustomerService
    {
        private delegate ValueTask<Customer> ReturningCustomerFunction();
        private delegate ValueTask<IQueryable<Customer>> ReturningCustomersFunction();

        private async ValueTask<Customer> TryCatch(ReturningCustomerFunction returningCustomerFunction)
        {
            try
            {
                return await returningCustomerFunction();
            }
            catch (NullCustomerException nullCustomerException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullCustomerException);
            }
            catch (InvalidCustomerException invalidCustomerException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidCustomerException);
            }
            catch (NotFoundCustomerException notFoundCustomerException)
            {
                throw await CreateAndLogValidationExceptionAsync(notFoundCustomerException);
            }
            catch (SqlException sqlException)
            {
                var failedStorageCustomerException = new FailedStorageCustomerException(
                    message: "Failed Customer storage error occurred, contact support.",
                    innerException: sqlException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(failedStorageCustomerException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsCustomerException =
                    new AlreadyExistsCustomerException(
                        message: "Customer already exists error occurred.",
                        innerException: duplicateKeyException,
                        data: duplicateKeyException.Data);

                throw await CreateAndLogDependencyValidationExceptionAsync(alreadyExistsCustomerException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedCustomerException =
                    new LockedCustomerException(
                        message: "Locked Customer record error occurred, please try again.",
                        innerException: dbUpdateConcurrencyException,
                        data: dbUpdateConcurrencyException.Data);

                throw await CreateAndLogDependencyValidationExceptionAsync(lockedCustomerException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                var failedOperationCustomerException =
                    new FailedOperationCustomerException(
                        message: "Failed operation Customer  error occurred, contact support.",
                        innerException: dbUpdateException);

                throw await CreateAndLogDependencyExceptionAsync(failedOperationCustomerException);
            }
            catch (Exception exception)
            {
                var failedServiceCustomerException =
                    new FailedServiceCustomerException(
                        message: "Failed service Customer error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedServiceCustomerException);
            }
        }

        private async ValueTask<IQueryable<Customer>> TryCatch(ReturningCustomersFunction returningCustomersFunction)
        {
            try
            {
                return await returningCustomersFunction();
            }
            catch (SqlException sqlException)
            {
                var failedStorageCustomerException = new FailedStorageCustomerException(
                    message: "Failed Customer storage error occurred, contact support.",
                    innerException: sqlException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(failedStorageCustomerException);
            }
            catch (Exception exception)
            {
                var failedServiceCustomerException =
                    new FailedServiceCustomerException(
                        message: "Failed service Customer error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedServiceCustomerException);
            }
        }

        private async ValueTask<CustomerValidationException> CreateAndLogValidationExceptionAsync(
            Xeption exception)
        {
            var CustomerValidationException = new CustomerValidationException(
                message: "Customer validation error occurred, fix errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(CustomerValidationException);

            return CustomerValidationException;
        }

        private async ValueTask<CustomerDependencyException> CreateAndLogCriticalDependencyExceptionAsync(
            Xeption exception)
        {
            var CustomerDependencyException = new CustomerDependencyException(
                message: "Customer dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogCriticalAsync(CustomerDependencyException);

            return CustomerDependencyException;
        }

        private async ValueTask<CustomerDependencyValidationException> CreateAndLogDependencyValidationExceptionAsync(
            Xeption exception)
        {
            var CustomerDependencyValidationException = new CustomerDependencyValidationException(
                message: "Customer dependency validation error occurred, fix errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(CustomerDependencyValidationException);

            return CustomerDependencyValidationException;
        }

        private async ValueTask<CustomerDependencyException> CreateAndLogDependencyExceptionAsync(
            Xeption exception)
        {
            var CustomerDependencyException = new CustomerDependencyException(
                message: "Customer dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(CustomerDependencyException);

            return CustomerDependencyException;
        }

        private async ValueTask<CustomerserviceException> CreateAndLogServiceExceptionAsync(
           Xeption exception)
        {
            var CustomerserviceException = new CustomerserviceException(
                message: "Service error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(CustomerserviceException);

            return CustomerserviceException;
        }
    }
}