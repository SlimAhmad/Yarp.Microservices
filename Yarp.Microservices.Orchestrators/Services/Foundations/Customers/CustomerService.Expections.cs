// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using RESTFulSense.Exceptions;
using Xeptions;
using Yarp.Microservices.Orchestrators.Models.Foundations.Customers;
using Yarp.Microservices.Orchestrators.Models.Foundations.Customers.Exceptions;

namespace Yarp.Microservices.Orchestrators.Services.Foundations.Customers
{
    public partial class CustomerService
    {
        private delegate ValueTask<Customer> ReturningCustomerFunction();
        private delegate ValueTask<List<Customer>> ReturningCustomersFunction();

        private async ValueTask<Customer> TryCatch(ReturningCustomerFunction returningCustomerFunction)
        {
            try
            {
                return await returningCustomerFunction();
            }
            catch (NullCustomerException nullCustomerException)
            {
                throw CreateAndLogValidationException(nullCustomerException);
            }
            catch (InvalidCustomerException invalidCustomerException)
            {
                throw CreateAndLogValidationException(invalidCustomerException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedCustomerDependencyException =
                    new FailedCustomerDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedCustomerDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedCustomerDependencyException =
                    new FailedCustomerDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedCustomerDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedCustomerDependencyException =
                    new FailedCustomerDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedCustomerDependencyException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundCustomerException =
                    new NotFoundCustomerException(httpResponseNotFoundException);

                throw CreateAndLogDependencyValidationException(notFoundCustomerException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidCustomerException =
                    new InvalidCustomerException(
                        httpResponseBadRequestException,
                        httpResponseBadRequestException.Data);

                throw CreateAndLogDependencyValidationException(invalidCustomerException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var invalidCustomerException =
                    new InvalidCustomerException(
                        httpResponseConflictException,
                        httpResponseConflictException.Data);

                throw CreateAndLogDependencyValidationException(invalidCustomerException);
            }
            catch (HttpResponseLockedException httpLockedException)
            {
                var lockedCustomerException =
                    new LockedCustomerException(httpLockedException);

                throw CreateAndLogDependencyValidationException(lockedCustomerException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedCustomerDependencyException =
                    new FailedCustomerDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedCustomerDependencyException);
            }
            catch (Exception exception)
            {
                var failedCustomerServiceException =
                    new FailedCustomerServiceException(exception);

                throw CreateAndLogCustomerServiceException(failedCustomerServiceException);
            }
        }

        private async ValueTask<List<Customer>> TryCatch(ReturningCustomersFunction returningCustomersFunction)
        {
            try
            {
                return await returningCustomersFunction();
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedCustomerDependencyException =
                    new FailedCustomerDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedCustomerDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedCustomerDependencyException =
                    new FailedCustomerDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedCustomerDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedCustomerDependencyException =
                    new FailedCustomerDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedCustomerDependencyException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedCustomerDependencyException =
                    new FailedCustomerDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedCustomerDependencyException);
            }
            catch (Exception exception)
            {
                var failedCustomerServiceException =
                    new FailedCustomerServiceException(exception);

                throw CreateAndLogCustomerServiceException(failedCustomerServiceException);
            }
        }

        private CustomerValidationException CreateAndLogValidationException(
            Exception exception)
        {
            var commentValidationException =
                new CustomerValidationException(exception);

            this.loggingBroker.LogErrorAsync(commentValidationException);

            return commentValidationException;
        }

        private CustomerDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var commentDependencyException =
                new CustomerDependencyException(exception);

            this.loggingBroker.LogCriticalAsync(commentDependencyException);

            return commentDependencyException;
        }

        private CustomerDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var commentDependencyValidationException =
                new CustomerDependencyValidationException(exception);

            this.loggingBroker.LogErrorAsync(commentDependencyValidationException);

            return commentDependencyValidationException;
        }

        private CustomerDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var commentDependencyException =
                new CustomerDependencyException(exception);

            this.loggingBroker.LogErrorAsync(commentDependencyException);

            return commentDependencyException;
        }

        private CustomerServiceException CreateAndLogCustomerServiceException(Xeption exception)
        {
            var commentServiceException =
                new CustomerServiceException(exception);

            this.loggingBroker.LogErrorAsync(commentServiceException);

            return commentServiceException;
        }
    }
}
