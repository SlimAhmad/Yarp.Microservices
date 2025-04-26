using Xeptions;
using Yarp.Microservices.Orchestrators.Models.Foundations.Orders;
using Yarp.Microservices.Orchestrators.Models.Foundations.Orders.Exceptions;
using Yarp.Microservices.Orchestrators.Models.Processings.Orders;

namespace Yarp.Microservices.Orchestrators.Services.Processings.Orders
{

    public partial class OrderProcessingService
    {
       
        private delegate ValueTask<bool> ReturningBooleanFunction();
        private delegate ValueTask<Order> ReturningOrderFunction();
        private delegate ValueTask<List<Order>> ReturningQueryableOrderFunction();

        private async ValueTask<bool> TryCatch(ReturningBooleanFunction returningBooleanFunction)
        {
            try
            {
                return await returningBooleanFunction();
            }
            catch (NullOrderProcessingException nullOrderProcessingException)
            {
                throw CreateAndLogValidationException(nullOrderProcessingException);
            }
            catch (InvalidOrderProcessingException invalidOrderProcessingException)
            {
                throw CreateAndLogValidationException(invalidOrderProcessingException);
            }
            catch (OrderValidationException orderValidationException)
            {
                throw CreateAndLogDependencyValidationException(orderValidationException);
            }
            catch (OrderDependencyValidationException OrderDependencyValidationException)
            {
                throw CreateAndLogDependencyValidationException(OrderDependencyValidationException);
            }
            catch (OrderDependencyException OrderDependencyException)
            {
                throw CreateAndLogDependencyException(OrderDependencyException);
            }
            catch (OrderServiceException OrderServiceException)
            {
                throw CreateAndLogDependencyException(OrderServiceException);
            }
            catch (Exception exception)
            {
                var failedOrderProcessingServiceException =
                    new FailedOrderProcessingServiceException(exception);

                throw CreateAndLogServiceException(failedOrderProcessingServiceException);
            }
        }
        private async ValueTask<Order> TryCatch(ReturningOrderFunction returningOrderFunction)
        {
            try
            {
                return await returningOrderFunction();
            }
            catch (NullOrderProcessingException nullOrderProcessingException)
            {
                throw CreateAndLogValidationException(nullOrderProcessingException);
            }
            catch (InvalidOrderProcessingException invalidOrderProcessingException)
            {
                throw CreateAndLogValidationException(invalidOrderProcessingException);
            }
            catch (OrderValidationException OrderValidationException)
            {
                throw CreateAndLogDependencyValidationException(OrderValidationException);
            }
            catch (OrderDependencyValidationException OrderDependencyValidationException)
            {
                throw CreateAndLogDependencyValidationException(OrderDependencyValidationException);
            }
            catch (OrderDependencyException OrderDependencyException)
            {
                throw CreateAndLogDependencyException(OrderDependencyException);
            }
            catch (OrderServiceException OrderServiceException)
            {
                throw CreateAndLogDependencyException(OrderServiceException);
            }
            catch (Exception exception)
            {
                var failedOrderProcessingServiceException =
                    new FailedOrderProcessingServiceException(exception);

                throw CreateAndLogServiceException(failedOrderProcessingServiceException);
            }
        }
        private ValueTask<List<Order>> TryCatch(ReturningQueryableOrderFunction returningQueryableOrderFunction)
        {
            try
            {
                return returningQueryableOrderFunction();
            }
            catch (NullOrderProcessingException nullOrderProcessingException)
            {
                throw CreateAndLogValidationException(nullOrderProcessingException);
            }
            catch (InvalidOrderProcessingException invalidOrderProcessingException)
            {
                throw CreateAndLogValidationException(invalidOrderProcessingException);
            }
            catch (OrderValidationException OrderValidationException)
            {
                throw CreateAndLogDependencyValidationException(OrderValidationException);
            }
            catch (OrderDependencyValidationException OrderDependencyValidationException)
            {
                throw CreateAndLogDependencyValidationException(OrderDependencyValidationException);
            }
            catch (OrderDependencyException OrderDependencyException)
            {
                throw CreateAndLogDependencyException(OrderDependencyException);
            }
            catch (OrderServiceException OrderServiceException)
            {
                throw CreateAndLogDependencyException(OrderServiceException);
            }
            catch (Exception exception)
            {
                var failedOrderProcessingServiceException =
                    new FailedOrderProcessingServiceException(exception);

                throw CreateAndLogServiceException(failedOrderProcessingServiceException);
            }
        }


        private OrderProcessingServiceException CreateAndLogServiceException(Xeption exception)
        {
            var OrderProcessingServiceException = new
                OrderProcessingServiceException(exception);

            this.loggingBroker.LogErrorAsync(OrderProcessingServiceException);

            return OrderProcessingServiceException;
        }

        private OrderProcessingDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var OrderProcessingDependencyValidationException =
                new OrderProcessingDependencyValidationException(
                    exception.InnerException as Xeption);

            this.loggingBroker.LogErrorAsync(OrderProcessingDependencyValidationException);

            return OrderProcessingDependencyValidationException;
        }

        private OrderProcessingDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var OrderProcessingDependencyException =
                new OrderProcessingDependencyException(
                    exception.InnerException as Xeption);

            this.loggingBroker.LogErrorAsync(OrderProcessingDependencyException);

            return OrderProcessingDependencyException;
        }

        private OrderProcessingValidationException CreateAndLogValidationException(
            Xeption exception)
        {
            var OrderProcessingValidationException =
                new OrderProcessingValidationException(exception);

            this.loggingBroker.LogErrorAsync(OrderProcessingValidationException);

            return OrderProcessingValidationException;
        }
    }
}