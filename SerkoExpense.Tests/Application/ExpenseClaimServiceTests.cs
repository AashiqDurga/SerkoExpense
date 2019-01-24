using System;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SerkoExpense.Application;
using SerkoExpense.Domain;
using SerkoExpense.Infrastructure;
using Xunit;

namespace SerkoExpense.Tests.Application
{
    public class ExpenseClaimServiceTests
    {
        private readonly ExpenseClaimService _expenseService;
        private Mock<IDataExtractor> _emailDataExtractor;
        private Mock<IExpenseClaimFactory> _expenseClaimFactory;
        private Mock<ILogger<IExpenseClaimService>> _logger;

        public ExpenseClaimServiceTests()
        {
            _emailDataExtractor = new Mock<IDataExtractor>();
            _emailDataExtractor.Setup(x => x.Extract(It.IsAny<string>())).Returns(new ExpenseClaimInput());
            _expenseClaimFactory = new Mock<IExpenseClaimFactory>();
            var expenseClaim = new ExpenseClaim("DEV002", 1024.01m, "personal card")
            {
                Date = new DateTime(2017, 04, 27), Description = "development team’s project end celebration dinner",
                Vendor = "Viaduct Steakhouse"
            };
            _expenseClaimFactory.Setup(x => x.CreateExpenseClaimFrom(It.IsAny<ExpenseClaimInput>()))
                .Returns(expenseClaim);

            _logger = new Mock<ILogger<IExpenseClaimService>>();
            _expenseService =
                new ExpenseClaimService(_emailDataExtractor.Object, _expenseClaimFactory.Object, _logger.Object);
        }

        [Fact]
        public void GivenAnExpenseClaimEmailWhenProcessingThenReturnTheCompleteClaim()
        {
            var expectedExpenseClaimResult = new ExpenseClaimResult
            {
                CostCentre = "DEV002", TotalIncludingGst = 1024.01m, TotalExcludingGst = 870.41m, GstAmount = 153.60m,
                PaymentMethod = "personal card", Description = "development team’s project end celebration dinner",
                Vendor = "Viaduct Steakhouse", Date = new DateTime(2017, 04, 27)
            };

            var email = @"Hi Yvaine,
            Please create an expense claim for the below. Relevant details are marked up as
                requested…
                <expense><cost_centre>DEV002</cost_centre>
                <total>1024.01</total><payment_method>personal card</payment_method>
                </expense>
                From: Ivan Castle
            Sent: Friday, 16 February 2018 10:32 AM
            To: Antoine Lloyd <Antoine.Lloyd@example.com>
                Subject: test
            Hi Antoine,
                Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our
                <description>development team’s project end celebration dinner</description> on
                <date>Thursday 27 April 2017</date>. We expect to arrive around
            7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.
                Regards,
            Ivan";

            var expenseClaimResult = _expenseService.Process(email);

            expenseClaimResult.Should().BeEquivalentTo(expectedExpenseClaimResult);
        }
    }
}