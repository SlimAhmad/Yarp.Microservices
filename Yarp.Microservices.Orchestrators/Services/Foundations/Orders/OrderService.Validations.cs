// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Yarp.Microservices.Orchestrators.Models.Foundations.Orders;
using Yarp.Microservices.Orchestrators.Models.Foundations.Orders.Exceptions;

namespace Yarp.Microservices.Orchestrators.Services.Foundations.Orders
{
    public partial class OrderService
    {
        private void ValidateOrderOnAdd(Order order)
        {
            ValidateOrderIsNotNull(order);

            Validate(
                (Rule: IsInvalid(order.Id), Parameter: nameof(Order.Id)),
                (Rule: IsInvalid(order.CustomerId), Parameter: nameof(Order.CustomerId)),
                (Rule: IsInvalid(order.ProductName), Parameter: nameof(Order.ProductName)),
                (Rule: IsInvalid(order.CreatedDate), Parameter: nameof(Order.CreatedDate)),
                (Rule: IsInvalid(order.UpdatedDate), Parameter: nameof(Order.UpdatedDate)));
        }

        private void ValidateOrderOnUpdate(Order order)
        {
            ValidateOrderIsNotNull(order);

            Validate(
                (Rule: IsInvalid(order.Id), Parameter: nameof(Order.Id)),
                (Rule: IsInvalid(order.CustomerId), Parameter: nameof(Order.CustomerId)),
                (Rule: IsInvalid(order.ProductName), Parameter: nameof(Order.ProductName)),
                (Rule: IsInvalid(order.CreatedDate), Parameter: nameof(Order.CreatedDate)),
                (Rule: IsInvalid(order.UpdatedDate), Parameter: nameof(Order.UpdatedDate)));
        }

        private static void ValidateOrderIsNotNull(Order order)
        {
            if (order is null)
            {
                throw new NullOrderException();
            }
        }

        public static void ValidateOrderId(Guid orderId) =>
            Validate((Rule: IsInvalid(orderId), Parameter: nameof(Order.Id)));

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "ProjectId is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidOrderException = new InvalidOrderException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidOrderException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidOrderException.ThrowIfContainsErrors();
        }
    }
}
