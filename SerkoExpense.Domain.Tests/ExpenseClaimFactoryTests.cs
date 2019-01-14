using System;
using FluentAssertions;
using SerkoExpense.Domain.Application;
using Xunit;

namespace SerkoExpense.Domain.Tests
{
    public class ExpenseClaimFactoryTests
    {
        private readonly ExpenseClaimFactory _expenseClaimFactory;

        public ExpenseClaimFactoryTests()
        {
            _expenseClaimFactory = new ExpenseClaimFactory();
        }

        [Fact]
        public void GivenAnExpenseClaimInputThenCreateAnExpenseClaim()
        {
            var expected = new ExpenseClaim
            {
                Expense = new Expense() {CostCentre = "DEV002", Total = 104.23m, PaymentMethod = "personal card"},
                Vendor = "Subway", Description = "Lunch Meeting",
                Date = new DateTime(2019, 01, 15)
            };
            
            var expenseInput = new ExpenseClaimInput
            {
                CostCentre = "DEV002", Total = 104.23m, PaymentMethod = "personal card", Vendor = "Subway",
                Description = "Lunch Meeting", Date = "Tuesday 15 January 2019"
            };

            var expenseClaim = _expenseClaimFactory.CreateExpenseClaim(expenseInput);
            
            expenseClaim.Should().BeEquivalentTo(expected);
        }
        
        [Fact]
        public void GivenAnIncorrectDateWhenProcessingTheContentThenThrowAnException()
        {
            var expenseInput = new ExpenseClaimInput
            {
                CostCentre = "DEV002", Total = 104.23m, PaymentMethod = "personal card", Vendor = "Subway",
                Description = "Lunch Meeting", Date = "Tuesday 27 April 2017"
            };

            Assert.Throws<FormatException>(() => _expenseClaimFactory.CreateExpenseClaim(expenseInput));
        }
    }
}