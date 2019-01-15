using System;
using System.Globalization;
using SerkoExpense.Domain;

namespace SerkoExpense.Application
{
    public class ExpenseClaimFactory
    {
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
                throw exception;
            }

            return dateTime;
        }
    }
}