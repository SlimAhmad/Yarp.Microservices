using Yarp.Microservices.Orchestrators.Models.Foundations.Orders;
using Yarp.Microservices.Orchestrators.Models.Processings.Orders;

namespace Yarp.Microservices.Orchestrators.Services.Processings.Orders
{
    public partial class OrderProcessingService
    {
        private static void ValidateOrder(Order order)
        {
            ValidateOrderIsNotNull(order);

            Validate(
            (Rule: IsInvalid(order.Id),
                Parameter: nameof(Order.Id)));
        }

        private static void ValidateOrderIsNotNull(Order  order)
        {
            if (order is null)
            {
                throw new NullOrderProcessingException();
            }
        }

        public void ValidateOrderId(Guid OrderId) =>
           Validate((Rule: IsInvalid(OrderId), Parameter: nameof(Order.Id)));

        private static dynamic IsInvalid(object @object) => new
        {
            Condition = @object is null,
            Message = "Value is required"
        };

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };
        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidOrderProcessingException = new InvalidOrderProcessingException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidOrderProcessingException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidOrderProcessingException.ThrowIfContainsErrors();
        }
    }
}