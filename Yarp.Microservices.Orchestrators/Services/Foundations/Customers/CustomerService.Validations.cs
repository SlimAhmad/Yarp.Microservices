// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Yarp.Microservices.Orchestrators.Models.Foundations.Customers;
using Yarp.Microservices.Orchestrators.Models.Foundations.Customers.Exceptions;

namespace Yarp.Microservices.Orchestrators.Services.Foundations.Customers
{
    public partial class CustomerService
    {
        private void ValidateCustomerOnAdd(Customer customer)
        {
            ValidateCustomerIsNotNull(customer);

            Validate(
                (Rule: IsInvalid(customer.Id), Parameter: nameof(Customer.Id)),
                (Rule: IsInvalid(customer.Name), Parameter: nameof(Customer.Name)),
                (Rule: IsInvalid(customer.CreatedDate), Parameter: nameof(Customer.CreatedDate)),
                (Rule: IsInvalid(customer.UpdatedDate), Parameter: nameof(Customer.UpdatedDate)));
        }

        private void ValidateCustomerOnUpdate(Customer customer)
        {
            ValidateCustomerIsNotNull(customer);

            Validate(
                (Rule: IsInvalid(customer.Id), Parameter: nameof(Customer.Id)),
                (Rule: IsInvalid(customer.Name), Parameter: nameof(Customer.Name)),
                (Rule: IsInvalid(customer.CreatedDate), Parameter: nameof(Customer.CreatedDate)),
                (Rule: IsInvalid(customer.UpdatedDate), Parameter: nameof(Customer.UpdatedDate)));
        }

        private static void ValidateCustomerIsNotNull(Customer customer)
        {
            if (customer is null)
            {
                throw new NullCustomerException();
            }
        }

        public static void ValidateCustomerId(Guid customerId) =>
            Validate((Rule: IsInvalid(customerId), Parameter: nameof(Customer.Id)));

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
            var invalidCustomerException = new InvalidCustomerException();

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
