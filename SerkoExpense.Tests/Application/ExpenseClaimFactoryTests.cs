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
        private Mock<IDateValidator> _validator;

        public ExpenseClaimFactoryTests()
        {
            _logger = new Mock<ILogger<IExpenseClaimFactory>>();
            _validator = new Mock<IDateValidator>();
            _expenseClaimFactory = new ExpenseClaimFactory(_validator.Object, _logger.Object);
        }

        [Fact]
        public void GivenAnExpenseClaimInputThenCreateAnExpenseClaim()
        {
            _validator.Setup(x => x.Validate(It.IsAny<string>())).Returns(new DateTime(2019, 01, 15));
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

    }
}