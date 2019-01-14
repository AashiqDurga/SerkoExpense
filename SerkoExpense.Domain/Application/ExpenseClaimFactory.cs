using System;
using System.Globalization;

namespace SerkoExpense.Domain.Application
{
    public class ExpenseClaimFactory
    {
        public ExpenseClaim CreateExpenseClaim(ExpenseClaimInput expenseInput)
        {
            var expenseInformation = new Expense
            {
                CostCentre = expenseInput.CostCentre, Total = expenseInput.Total,
                PaymentMethod = expenseInput.PaymentMethod
            };

            var dateTime = ValidateDate(expenseInput);
            var expenseClaim = new ExpenseClaim
            {
                Expense = expenseInformation, Vendor = expenseInput.Vendor, Date = dateTime,
                Description = expenseInput.Description
            };

            return expenseClaim;
        }

        private static DateTime ValidateDate(ExpenseClaimInput expenseInput)
        {
            DateTime dateTime;
            try
            {
                const string supportedDateFormat = "dddd d MMMM yyyy";
                dateTime = DateTime.ParseExact((string) expenseInput.Date, supportedDateFormat,
                    CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                throw e;
            }

            return dateTime;
        }
    }
}