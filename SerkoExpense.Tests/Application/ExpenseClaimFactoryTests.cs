using System;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SerkoExpense.Application;
using SerkoExpense.Domain;
using Xunit;

namespace SerkoExpense.Tests.Application
{
    public class ExpenseClaimFactoryTests
    {
        private readonly ExpenseClaimFactory _expenseClaimFactory;
        private Mock<ILogger<IExpenseClaimFactory>> _logger;

        public ExpenseClaimFactoryTests()
        {
            _logger = new Mock<ILogger<IExpenseClaimFactory>>();
            _expenseClaimFactory = new ExpenseClaimFactory(_logger.Object);
        }

        [Fact]
        public void GivenAnExpenseClaimInputThenCreateAnExpenseClaim()
        {
            var expectedExpenseClaim = new ExpenseClaim("DEV002", 104.23m, "personal card")
            {
                Vendor = "Subway", Description = "Lunch Meeting",
                Date = new DateTime(2019, 01, 15)
            };

            var expenseInput = new ExpenseClaimInput
            {
                CostCentre = "DEV002", Total = 104.23m, PaymentMethod = "personal card", Vendor = "Subway",
                Description = "Lunch Meeting", Date = "Tuesday 15 January 2019"
            };

            var expenseClaim = _expenseClaimFactory.CreateExpenseClaimFrom(expenseInput);

            expenseClaim.Should().BeEquivalentTo(expectedExpenseClaim);
        }

        [Fact]
        public void GivenAnIncorrectDateWhenProcessingTheContentThenThrowAnException()
        {
            var expenseInput = new ExpenseClaimInput
            {
                CostCentre = "DEV002", Total = 104.23m, PaymentMethod = "personal card", Vendor = "Subway",
                Description = "Lunch Meeting", Date = "Tuesday 27 April 2017"
            };

            var exception =
                Assert.Throws<InvalidDateException>(() => _expenseClaimFactory.CreateExpenseClaimFrom(expenseInput));
            Assert.Equal("The date supplied is Invalid.", exception.Message);
        }
    }
}