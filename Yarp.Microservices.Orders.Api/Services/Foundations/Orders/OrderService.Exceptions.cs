// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using EFxceptions.Models.Exceptions;
using GitFyle.Core.Api.Models.Foundations.Orders;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Xeptions;
using Yarp.Microservices.Orders.Api.Models;
using Yarp.Microservices.Orders.Api.Models.Foundations.Orders.Exceptions;
using Yarp.Microservices.Orders.Api.Tests.Unit.Services.Foundations.Orders;

namespace Yarp.Microservices.Orders.Api.Services.Foundations.Orders
{
    internal partial class OrderService
    {
        private delegate ValueTask<Order> ReturningOrderFunction();
        private delegate ValueTask<IQueryable<Order>> ReturningOrdersFunction();

        private async ValueTask<Order> TryCatch(ReturningOrderFunction returningOrderFunction)
        {
            try
            {
                return await returningOrderFunction();
            }
            catch (NullOrderException nullOrderException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullOrderException);
            }
            catch (InvalidOrderException invalidOrderException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidOrderException);
            }
            catch (NotFoundOrderException notFoundOrderException)
            {
                throw await CreateAndLogValidationExceptionAsync(notFoundOrderException);
            }
            catch (SqlException sqlException)
            {
                var failedStorageOrderException = new FailedStorageOrderException(
                    message: "Failed Order storage error occurred, contact support.",
                    innerException: sqlException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(failedStorageOrderException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsOrderException =
                    new AlreadyExistsOrderException(
                        message: "Order already exists error occurred.",
                        innerException: duplicateKeyException,
                        data: duplicateKeyException.Data);

                throw await CreateAndLogDependencyValidationExceptionAsync(alreadyExistsOrderException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedOrderException =
                    new LockedOrderException(
                        message: "Locked Order record error occurred, please try again.",
                        innerException: dbUpdateConcurrencyException,
                        data: dbUpdateConcurrencyException.Data);

                throw await CreateAndLogDependencyValidationExceptionAsync(lockedOrderException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                var failedOperationOrderException =
                    new FailedOperationOrderException(
                        message: "Failed operation Order  error occurred, contact support.",
                        innerException: dbUpdateException);

                throw await CreateAndLogDependencyExceptionAsync(failedOperationOrderException);
            }
            catch (Exception exception)
            {
                var failedServiceOrderException =
                    new FailedServiceOrderException(
                        message: "Failed service Order error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedServiceOrderException);
            }
        }

        private async ValueTask<IQueryable<Order>> TryCatch(ReturningOrdersFunction returningOrdersFunction)
        {
            try
            {
                return await returningOrdersFunction();
            }
            catch (SqlException sqlException)
            {
                var failedStorageOrderException = new FailedStorageOrderException(
                    message: "Failed Order storage error occurred, contact support.",
                    innerException: sqlException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(failedStorageOrderException);
            }
            catch (Exception exception)
            {
                var failedServiceOrderException =
                    new FailedServiceOrderException(
                        message: "Failed service Order error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedServiceOrderException);
            }
        }

        private async ValueTask<OrderValidationException> CreateAndLogValidationExceptionAsync(
            Xeption exception)
        {
            var OrderValidationException = new OrderValidationException(
                message: "Order validation error occurred, fix errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(OrderValidationException);

            return OrderValidationException;
        }

        private async ValueTask<OrderDependencyException> CreateAndLogCriticalDependencyExceptionAsync(
            Xeption exception)
        {
            var OrderDependencyException = new OrderDependencyException(
                message: "Order dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogCriticalAsync(OrderDependencyException);

            return OrderDependencyException;
        }

        private async ValueTask<OrderDependencyValidationException> CreateAndLogDependencyValidationExceptionAsync(
            Xeption exception)
        {
            var OrderDependencyValidationException = new OrderDependencyValidationException(
                message: "Order dependency validation error occurred, fix errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(OrderDependencyValidationException);

            return OrderDependencyValidationException;
        }

        private async ValueTask<OrderDependencyException> CreateAndLogDependencyExceptionAsync(
            Xeption exception)
        {
            var OrderDependencyException = new OrderDependencyException(
                message: "Order dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(OrderDependencyException);

            return OrderDependencyException;
        }

        private async ValueTask<OrderServiceException> CreateAndLogServiceExceptionAsync(
           Xeption exception)
        {
            var OrderServiceException = new OrderServiceException(
                message: "Service error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(OrderServiceException);

            return OrderServiceException;
        }
    }
}