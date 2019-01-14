using System;
using System.Globalization;
using FluentAssertions;
using Xunit;

namespace SerkoExpense.Domain.Tests
{
    public class ExpenseClaimFactoryTests
    {
        [Fact]
        public void GivenAnExpenseClaimInputThenCreateAnExpenseClaim()
        {
            var expected = new ExpenseClaim
            {
                ExpenseInformation = new Expense() {CostCentre = "DEV002", Total = 104.23m, PaymentMethod = "personal card"},
                Vendor = "Subway", Description = "Lunch Meeting",
                Date = new DateTime(2019, 01, 15)
            };
            
            var expenseInput = new ExpenseClaimInput
            {
                CostCentre = "DEV002", Total = 104.23m, PaymentMethod = "personal card", Vendor = "Subway",
                Description = "Lunch Meeting", Date = "Tuesday 15 January 2019"
            };

            var expenseClaimFactory = new ExpenseClaimFactory();

            var expenseClaim = expenseClaimFactory.CreateExpenseClaim(expenseInput);
            
            expenseClaim.Should().BeEquivalentTo(expected);
        }
    }

    public class ExpenseClaimFactory
    {
        public ExpenseClaim CreateExpenseClaim(ExpenseClaimInput expenseInput)
        {
            var expenseInformation = new Expense
            {
                CostCentre = expenseInput.CostCentre, Total = expenseInput.Total,
                PaymentMethod = expenseInput.PaymentMethod
            };

            const string supportedDateFormat = "dddd d MMMM yyyy";
            var dateTime = DateTime.ParseExact(expenseInput.Date, supportedDateFormat, CultureInfo.InvariantCulture);
            var expenseClaim = new ExpenseClaim()
            {
                ExpenseInformation = expenseInformation, Vendor = expenseInput.Vendor, Date = dateTime,
                Description = expenseInput.Description
            };

            return expenseClaim;
        }
    }

    public class ExpenseClaimInput
    {
        public string CostCentre { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
    }
}