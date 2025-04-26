// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using GitFyle.Core.Api.Models.Foundations.Orders;
using Yarp.Microservices.Orders.Api.Models.Foundations.Orders.Exceptions;

namespace Yarp.Microservices.Orders.Api.Services.Foundations.Orders
{
    internal partial class OrderService
    {
        private async ValueTask ValidateOrderOnAddAsync(Order order)
        {
            ValidateOrderIsNotNull(order);

            Validate(
                (Rule: IsInvalid(order.Id), Parameter: nameof(Order.Id)),
                (Rule: IsInvalid(order.ProductName), Parameter: nameof(Order.ProductName)),
                (Rule: IsInvalid(order.CustomerId), Parameter: nameof(Order.CustomerId)),
                (Rule: IsInvalid(order.CreatedBy), Parameter: nameof(Order.CreatedBy)),
                (Rule: IsInvalid(order.CreatedDate), Parameter: nameof(Order.CreatedDate)),
                (Rule: IsInvalid(order.UpdatedBy), Parameter: nameof(Order.UpdatedBy)),
                (Rule: IsInvalid(order.UpdatedDate), Parameter: nameof(Order.UpdatedDate)),
                (Rule: IsInvalidLength(order.ProductName, 255), Parameter: nameof(Order.ProductName)),

                (Rule: IsNotSame(
                    first: order.UpdatedBy,
                    second: order.CreatedBy,
                    secondName: nameof(Order.CreatedBy)),

                Parameter: nameof(Order.UpdatedBy)),

                (Rule: IsNotSame(
                    firstDate: order.UpdatedDate,
                    secondDate: order.CreatedDate,
                    secondDateName: nameof(Order.CreatedDate)),

                Parameter: nameof(Order.UpdatedDate)),

                (Rule: await IsNotRecentAsync(order.CreatedDate), Parameter: nameof(Order.CreatedDate)));
        }

        private async ValueTask ValidateOrderOnModifyAsync(Order order)
        {
            ValidateOrderIsNotNull(order);

            Validate(
                (Rule: IsInvalid(order.Id), Parameter: nameof(Order.Id)),
                (Rule: IsInvalid(order.ProductName), Parameter: nameof(Order.ProductName)),
                (Rule: IsInvalid(order.CustomerId), Parameter: nameof(Order.CustomerId)),
                (Rule: IsInvalid(order.CreatedBy), Parameter: nameof(Order.CreatedBy)),
                (Rule: IsInvalid(order.CreatedDate), Parameter: nameof(Order.CreatedDate)),
                (Rule: IsInvalid(order.UpdatedBy), Parameter: nameof(Order.UpdatedBy)),
                (Rule: IsInvalid(order.UpdatedDate), Parameter: nameof(Order.UpdatedDate)),
                (Rule: IsInvalidLength(order.ProductName, 255), Parameter: nameof(Order.ProductName)),

                (Rule: IsSame(
                    firstDate: order.UpdatedDate,
                    secondDate: order.CreatedDate,
                    secondDateName: nameof(Order.CreatedDate)),

                Parameter: nameof(Order.UpdatedDate)),

                (Rule: await IsNotRecentAsync(order.UpdatedDate), Parameter: nameof(Order.UpdatedDate)));
        }

        private async ValueTask<dynamic> IsNotRecentAsync(DateTimeOffset date)
        {
            var (isNotRecent, startDate, endDate) = await IsDateNotRecentAsync(date);

            return new
            {
                Condition = isNotRecent,
                Message = $"Date is not recent. Expected a value between {startDate} and {endDate} but found {date}"
            };
        }

        private async ValueTask<(bool IsNotRecent, DateTimeOffset StartDate, DateTimeOffset EndDate)>
            IsDateNotRecentAsync(DateTimeOffset date)
        {
            int pastSeconds = 60;
            int futureSeconds = 0;

            DateTimeOffset currentDateTime =
                await this.dateTimeBroker.GetCurrentDateTimeOffsetAsync();

            if (currentDateTime == default)
            {
                return (false, default, default);
            }

            TimeSpan timeDifference = currentDateTime.Subtract(date);
            DateTimeOffset startDate = currentDateTime.AddSeconds(-pastSeconds);
            DateTimeOffset endDate = currentDateTime.AddSeconds(futureSeconds);
            bool isNotRecent = timeDifference.TotalSeconds is > 60 or < 0;

            return (isNotRecent, startDate, endDate);
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is invalid"
        };

        private static dynamic IsInvalid(string name) => new
        {
            Condition = String.IsNullOrWhiteSpace(name),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is invalid"
        };

        private static dynamic IsInvalidLength(string text, int maxLength) => new
        {
            Condition = IsExceedingLength(text, maxLength),
            Message = $"Text exceed max length of {maxLength} characters"
        };

        private static bool IsExceedingLength(string text, int maxLength) =>
            (text ?? string.Empty).Length > maxLength;

        private static dynamic IsInvalidUrl(string url) => new
        {
            Condition = IsValidUrl(url) is false,
            Message = "Url is invalid"
        };

        private static dynamic IsSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate == secondDate,
                Message = $"Date is the same as {secondDateName}"
            };

        private static dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not the same as {secondDateName}"
            };

        private static dynamic IsNotSame(
            string first,
            string second,
            string secondName) => new
            {
                Condition = first != second,
                Message = $"Text is not the same as {secondName}"
            };

        private static bool IsValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }

            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult))
            {
                return false;
            }

            return uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
        }

        private static void ValidateOrderId(Guid orderId) =>
            Validate((Rule: IsInvalid(orderId), Parameter: nameof(Order.Id)));

        private static void ValidateOrderIsNotNull(Order order)
        {
            if (order is null)
            {
                throw new NullOrderException(message: "Order is null");
            }
        }

        private static void ValidateStorageOrder(Order maybeOrder, Guid id)
        {
            if (maybeOrder is null)
            {
                throw new NotFoundOrderException(
                    message: $"Order not found with id: {id}");
            }
        }

        private static void ValidateAgainstStorageOrderOnModify(
            Order inputOrder, Order storageOrder)
        {
            Validate(
                (Rule: IsNotSame(
                    first: inputOrder.CreatedBy,
                    second: storageOrder.CreatedBy,
                    secondName: nameof(Order.CreatedBy)),

                Parameter: nameof(Order.CreatedBy)),

                (Rule: IsNotSame(
                    firstDate: inputOrder.CreatedDate,
                    secondDate: storageOrder.CreatedDate,
                    secondDateName: nameof(Order.CreatedDate)),

                Parameter: nameof(Order.CreatedDate)),

                (Rule: IsSame(
                    firstDate: inputOrder.UpdatedDate,
                    secondDate: storageOrder.UpdatedDate,
                    secondDateName: nameof(Order.UpdatedDate)),

                Parameter: nameof(Order.UpdatedDate)));
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidOrderException = new InvalidOrderException(
                message: "Order is invalid, fix the errors and try again.");

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