using System;
using System.Globalization;
using Microsoft.Extensions.Logging;
using SerkoExpense.Domain;

namespace SerkoExpense.Application
{
    public class ExpenseClaimFactory : IExpenseClaimFactory
    {
        private static ILogger _logger;

        public ExpenseClaimFactory(ILogger<IExpenseClaimFactory> logger)
        {
            _logger = logger;
        }

        public ExpenseClaim CreateExpenseClaimFrom(ExpenseClaimInput expenseInput)
        {
            var expenseClaimDate = ValidateDate(expenseInput.Date);

            var expenseClaim = new ExpenseClaim(expenseInput.CostCentre, expenseInput.Total, expenseInput.PaymentMethod)
            {
                Vendor = expenseInput.Vendor, Date = expenseClaimDate, Description = expenseInput.Description
            };

            return expenseClaim;
        }

        private static DateTime ValidateDate(string date)
        {
            DateTime dateTime;
            try
            {
                const string supportedDateFormat = "dddd d MMMM yyyy";
                dateTime = DateTime.ParseExact(date, supportedDateFormat,
                    CultureInfo.InvariantCulture);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Failed to validate date {exception}");
                throw new InvalidDateException("The date supplied is Invalid.");
            }

            return dateTime;
        }
    }
}