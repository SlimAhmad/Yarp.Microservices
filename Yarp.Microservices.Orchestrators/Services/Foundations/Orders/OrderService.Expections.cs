// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using RESTFulSense.Exceptions;
using Xeptions;
using Yarp.Microservices.Orchestrators.Models.Foundations.Orders;
using Yarp.Microservices.Orchestrators.Models.Foundations.Orders.Exceptions;

namespace Yarp.Microservices.Orchestrators.Services.Foundations.Orders
{
    public partial class OrderService
    {
        private delegate ValueTask<Order> ReturningOrderFunction();
        private delegate ValueTask<List<Order>> ReturningOrdersFunction();

        private async ValueTask<Order> TryCatch(ReturningOrderFunction returningOrderFunction)
        {
            try
            {
                return await returningOrderFunction();
            }
            catch (NullOrderException nullOrderException)
            {
                throw CreateAndLogValidationException(nullOrderException);
            }
            catch (InvalidOrderException invalidOrderException)
            {
                throw CreateAndLogValidationException(invalidOrderException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedOrderDependencyException =
                    new FailedOrderDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedOrderDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedOrderDependencyException =
                    new FailedOrderDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedOrderDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedOrderDependencyException =
                    new FailedOrderDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedOrderDependencyException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundOrderException =
                    new NotFoundOrderException(httpResponseNotFoundException);

                throw CreateAndLogDependencyValidationException(notFoundOrderException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidOrderException =
                    new InvalidOrderException(
                        httpResponseBadRequestException,
                        httpResponseBadRequestException.Data);

                throw CreateAndLogDependencyValidationException(invalidOrderException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var invalidOrderException =
                    new InvalidOrderException(
                        httpResponseConflictException,
                        httpResponseConflictException.Data);

                throw CreateAndLogDependencyValidationException(invalidOrderException);
            }
            catch (HttpResponseLockedException httpLockedException)
            {
                var lockedOrderException =
                    new LockedOrderException(httpLockedException);

                throw CreateAndLogDependencyValidationException(lockedOrderException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedOrderDependencyException =
                    new FailedOrderDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedOrderDependencyException);
            }
            catch (Exception exception)
            {
                var failedOrderServiceException =
                    new FailedOrderServiceException(exception);

                throw CreateAndLogOrderServiceException(failedOrderServiceException);
            }
        }

        private async ValueTask<List<Order>> TryCatch(ReturningOrdersFunction returningOrdersFunction)
        {
            try
            {
                return await returningOrdersFunction();
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedOrderDependencyException =
                    new FailedOrderDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedOrderDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedOrderDependencyException =
                    new FailedOrderDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedOrderDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedOrderDependencyException =
                    new FailedOrderDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedOrderDependencyException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedOrderDependencyException =
                    new FailedOrderDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedOrderDependencyException);
            }
            catch (Exception exception)
            {
                var failedOrderServiceException =
                    new FailedOrderServiceException(exception);

                throw CreateAndLogOrderServiceException(failedOrderServiceException);
            }
        }

        private OrderValidationException CreateAndLogValidationException(
            Exception exception)
        {
            var commentValidationException =
                new OrderValidationException(exception);

            this.loggingBroker.LogErrorAsync(commentValidationException);

            return commentValidationException;
        }

        private OrderDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var commentDependencyException =
                new OrderDependencyException(exception);

            this.loggingBroker.LogCriticalAsync(commentDependencyException);

            return commentDependencyException;
        }

        private OrderDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var commentDependencyValidationException =
                new OrderDependencyValidationException(exception);

            this.loggingBroker.LogErrorAsync(commentDependencyValidationException);

            return commentDependencyValidationException;
        }

        private OrderDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var commentDependencyException =
                new OrderDependencyException(exception);

            this.loggingBroker.LogErrorAsync(commentDependencyException);

            return commentDependencyException;
        }

        private OrderServiceException CreateAndLogOrderServiceException(Xeption exception)
        {
            var commentServiceException =
                new OrderServiceException(exception);

            this.loggingBroker.LogErrorAsync(commentServiceException);

            return commentServiceException;
        }
    }
}
