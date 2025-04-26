// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Yarp.Microservices.Customers.Api.Models.Foundations.Customers;
using Yarp.Microservices.Customers.Api.Models.Foundations.Customers.Exceptions;

namespace Yarp.Microservices.Customers.Api.Services.Foundations.Customers
{
    internal partial class CustomerService
    {
        private async ValueTask ValidateCustomerOnAddAsync(Customer Customer)
        {
            ValidateCustomerIsNotNull(Customer);

            Validate(
                (Rule: IsInvalid(Customer.Id), Parameter: nameof(Customer.Id)),
                (Rule: IsInvalid(Customer.Name), Parameter: nameof(Customer.Name)),
                (Rule: IsInvalid(Customer.CreatedBy), Parameter: nameof(Customer.CreatedBy)),
                (Rule: IsInvalid(Customer.CreatedDate), Parameter: nameof(Customer.CreatedDate)),
                (Rule: IsInvalid(Customer.UpdatedBy), Parameter: nameof(Customer.UpdatedBy)),
                (Rule: IsInvalid(Customer.UpdatedDate), Parameter: nameof(Customer.UpdatedDate)),
                (Rule: IsInvalidLength(Customer.Name, 255), Parameter: nameof(Customer.Name)),

                (Rule: IsNotSame(
                    first: Customer.UpdatedBy,
                    second: Customer.CreatedBy,
                    secondName: nameof(Customer.CreatedBy)),

                Parameter: nameof(Customer.UpdatedBy)),

                (Rule: IsNotSame(
                    firstDate: Customer.UpdatedDate,
                    secondDate: Customer.CreatedDate,
                    secondDateName: nameof(Customer.CreatedDate)),

                Parameter: nameof(Customer.UpdatedDate)),

                (Rule: await IsNotRecentAsync(Customer.CreatedDate), Parameter: nameof(Customer.CreatedDate)));
        }

        private async ValueTask ValidateCustomerOnModifyAsync(Customer Customer)
        {
            ValidateCustomerIsNotNull(Customer);

            Validate(
                (Rule: IsInvalid(Customer.Id), Parameter: nameof(Customer.Id)),
                (Rule: IsInvalid(Customer.Name), Parameter: nameof(Customer.Name)),
                (Rule: IsInvalid(Customer.CreatedBy), Parameter: nameof(Customer.CreatedBy)),
                (Rule: IsInvalid(Customer.CreatedDate), Parameter: nameof(Customer.CreatedDate)),
                (Rule: IsInvalid(Customer.UpdatedBy), Parameter: nameof(Customer.UpdatedBy)),
                (Rule: IsInvalid(Customer.UpdatedDate), Parameter: nameof(Customer.UpdatedDate)),
                (Rule: IsInvalidLength(Customer.Name, 255), Parameter: nameof(Customer.Name)),

                (Rule: IsSame(
                    firstDate: Customer.UpdatedDate,
                    secondDate: Customer.CreatedDate,
                    secondDateName: nameof(Customer.CreatedDate)),

                Parameter: nameof(Customer.UpdatedDate)),

                (Rule: await IsNotRecentAsync(Customer.UpdatedDate), Parameter: nameof(Customer.UpdatedDate)));
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

        private static void ValidateCustomerId(Guid CustomerId) =>
            Validate((Rule: IsInvalid(CustomerId), Parameter: nameof(Customer.Id)));

        private static void ValidateCustomerIsNotNull(Customer Customer)
        {
            if (Customer is null)
            {
                throw new NullCustomerException(message: "Customer is null");
            }
        }

        private static void ValidateStorageCustomer(Customer maybeCustomer, Guid id)
        {
            if (maybeCustomer is null)
            {
                throw new NotFoundCustomerException(
                    message: $"Customer not found with id: {id}");
            }
        }

        private static void ValidateAgainstStorageCustomerOnModify(
            Customer inputCustomer, Customer storageCustomer)
        {
            Validate(
                (Rule: IsNotSame(
                    first: inputCustomer.CreatedBy,
                    second: storageCustomer.CreatedBy,
                    secondName: nameof(Customer.CreatedBy)),

                Parameter: nameof(Customer.CreatedBy)),

                (Rule: IsNotSame(
                    firstDate: inputCustomer.CreatedDate,
                    secondDate: storageCustomer.CreatedDate,
                    secondDateName: nameof(Customer.CreatedDate)),

                Parameter: nameof(Customer.CreatedDate)),

                (Rule: IsSame(
                    firstDate: inputCustomer.UpdatedDate,
                    secondDate: storageCustomer.UpdatedDate,
                    secondDateName: nameof(Customer.UpdatedDate)),

                Parameter: nameof(Customer.UpdatedDate)));
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidCustomerException = new InvalidCustomerException(
                message: "Customer is invalid, fix the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidCustomerException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidCustomerException.ThrowIfContainsErrors();
        }
    }
}